import { Directive, EmbeddedViewRef, Input, TemplateRef, ViewContainerRef, OnDestroy } from '@angular/core';
import { AuthService } from '../auth.service';
import { Subscription } from 'rxjs';

@Directive({
  selector: '[ifAuthenticated]'
})
export class IfAuthenticatedDirective implements OnDestroy {

  private _context: IfAuthenticatedContext = new IfAuthenticatedContext();
  private _thenTemplateRef: TemplateRef<IfAuthenticatedContext> | null = null;
  private _thenViewRef: EmbeddedViewRef<IfAuthenticatedContext> | null = null;
  private CheckifUnauthenticated = false;
  checkLoginSubscription: Subscription;

  // tslint:disable-next-line:max-line-length
  constructor(private authService: AuthService, private _viewContainer: ViewContainerRef, templateRef: TemplateRef<IfAuthenticatedContext>) {
    this._thenTemplateRef = templateRef;

  }

  @Input()
  set ifAuthenticated(condition: any) {
    if (condition === false || condition === 'false') {
      this.CheckifUnauthenticated = true;
    }

    if (this.CheckifUnauthenticated) {
      this._context.$implicit = this._context.ifAuthenticated = !this.authService.checkLogin();
    } else {
      this._context.$implicit = this._context.ifAuthenticated = this.authService.checkLogin();
    }
    this.checkLoginSubscription = this.authService.getUserInfo().subscribe(
      result => {
        if (this.CheckifUnauthenticated) {
          this._context.$implicit = this._context.ifAuthenticated = !result.authenticated;
        } else {
          this._context.$implicit = this._context.ifAuthenticated = result.authenticated;
        }
        this._updateView();
      },
      error => { }
    );
    this._updateView();
  }

  private _updateView() {
    if (this._context.$implicit) {
      if (!this._thenViewRef) {
        this._viewContainer.clear();
        if (this._thenTemplateRef) {
          this._thenViewRef = this._viewContainer.createEmbeddedView(this._thenTemplateRef, this._context);
        }
      }
    } else {
      this._viewContainer.clear();
      this._thenViewRef = null;
    }
  }

  ngOnDestroy(): void {
    if (this.checkLoginSubscription) { this.checkLoginSubscription.unsubscribe(); }
  }
}

export class IfAuthenticatedContext {
  public $implicit: any = null;
  public ifAuthenticated: any = null;
}
