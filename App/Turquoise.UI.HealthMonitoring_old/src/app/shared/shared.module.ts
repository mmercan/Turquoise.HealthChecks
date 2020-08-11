import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthenticationModule } from './authentication/authentication.module';
import { NamespaceNgService } from './grpc_services/namespace.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AuthenticationModule,
  ],
  providers: [
    NamespaceNgService,
  ]
})
export class SharedModule {

  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [NamespaceNgService],
    };
  }

}
