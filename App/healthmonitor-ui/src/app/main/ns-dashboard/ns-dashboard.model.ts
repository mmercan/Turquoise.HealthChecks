import { DeploymentReply } from 'app/proto/K8sHealthcheck_pb';

export class NSDashboardResponse {

    currentNamespace: string;
    events: any[];
    services: any[];
    stats: any;
    deployments: DeploymentReply[];
}

