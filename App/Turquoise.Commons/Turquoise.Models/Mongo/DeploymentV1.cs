using System;
using System.Collections.Generic;

namespace Turquoise.Models.Mongo
{
    public class DeploymentV1
    {
        public string Kind { get; set; }
        public MetadataV1 Metadata { get; set; }
        public DeploymentSpecV1 Spec { get; set; }
        public DeploymentStatusV1 Status { get; set; }
    }

    public class MetadataV1
    {
        public List<Label> Annotations { get; set; }
        public DateTime? CreationTime { get; set; }
        public int Generation { get; set; }
        public List<Label> Labels { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string ResourceVersion { get; set; }
        public string Uid { get; set; }
        public IList<OwnerReferenceV1> OwnerReferences { get; set; }
    }


    public class DeploymentSpecV1
    {
        public int ProgressDeadlineSeconds { get; set; }
        public int Replicas { get; set; }
        public int RevisionHistoryLimit { get; set; }
        public List<Label> Selector { get; set; }
        public string SelectorString { get; set; }
        public PodTemplateSpecV1 Template { get; set; }
    }


    public class PodTemplateSpecV1
    {
        public MetadataV1 Metadata { get; set; }
        public PodSpecV1 Spec { get; set; }
    }

    public class PodSpecV1
    {
        public string PreemptionPolicy { get; set; }
        public int? Priority { get; set; }

        public string PriorityClassName { get; set; }

        public string RestartPolicy { get; set; }
        public string RuntimeClassName { get; set; }

        public string SchedulerName { get; set; }

        public string ServiceAccount { get; set; }
        public string ServiceAccountName { get; set; }
        public bool? ShareProcessNamespace { get; set; }
        public string Subdomain { get; set; }
        public long? TerminationGracePeriodSeconds { get; set; }

        public List<Label> NodeSelector { get; set; }
        public string NodeName { get; set; }
        public long? ActiveDeadlineSeconds { get; set; }
        public bool? AutomountServiceAccountToken { get; set; }

        public IList<ContainerV1> Containers { get; set; }



        // public string DnsPolicy { get; set; }
        // public bool? EnableServiceLinks { get; set; }
        // public bool? HostIPC { get; set; }
        // public bool? HostNetwork { get; set; }
        // public bool? HostPID { get; set; }
        // public string Hostname { get; set; }

        // public IList<V1LocalObjectReference> ImagePullSecrets { get; set; }

        // public IList<V1EphemeralContainer> EphemeralContainers { get; set; }

        // public IList<V1Volume> Volumes { get; set; }


    }


    public class DeploymentStatusV1
    {
        public int? AvailableReplicas { get; set; }
        public int? CollisionCount { get; set; }
        public IList<DeploymentConditionV1> Conditions { get; set; }
        public long? ObservedGeneration { get; set; }
        public int? ReadyReplicas { get; set; }
        public int? Replicas { get; set; }
        public int? UnavailableReplicas { get; set; }
        public int? UpdatedReplicas { get; set; }
    }

    public class DeploymentConditionV1
    {
        public DateTime? LastTransitionTime { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public string Message { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }

}



//  {
//     "apiVersion": null,
//     "kind": null,
//     "metadata": {
//       "annotations": {
//         "azure-pipelines/jobName": "deploy Web App",
//         "azure-pipelines/org": "https://bupaaunz.visualstudio.com/",
//         "azure-pipelines/pipeline": "Sentinel.UI.Admin",
//         "azure-pipelines/pipelineId": "3151",
//         "azure-pipelines/project": "AKS_Experiment",
//         "azure-pipelines/run": "20200907.2",
//         "azure-pipelines/runuri": "https://bupaaunz.visualstudio.com/AKS_Experiment/_build/results?buildId=248366",
//         "deployment.kubernetes.io/revision": "1"
//       },
//       "clusterName": null,
//       "creationTimestamp": "2020-09-07T08:16:34Z",
//       "deletionGracePeriodSeconds": null,
//       "deletionTimestamp": null,
//       "finalizers": null,
//       "generateName": null,
//       "generation": 1,
//       "labels": {
//         "app": "admin-ui",
//         "app.kubernetes.io/instance": "sentinel-dev-admin-ui",
//         "app.kubernetes.io/managed-by": "Helm",
//         "app.kubernetes.io/name": "admin-ui",
//         "app.kubernetes.io/version": "1.0.0",
//         "branch": "master",
//         "helm.sh/chart": "admin-ui-2.0.0-20200907.2",
//         "version": "1.0.0"
//       },
//       "managedFields": null,
//       "name": "sentinel-dev-admin-ui",
//       "namespaceProperty": "sentinel-dev",
//       "ownerReferences": null,
//       "resourceVersion": "548586",
//       "selfLink": "/apis/apps/v1/namespaces/sentinel-dev/deployments/sentinel-dev-admin-ui",
//       "uid": "b6fede68-3f4b-4bbf-8d50-8028f6a398ee"
//     },
//     "spec": {
//       "minReadySeconds": null,
//       "paused": null,
//       "progressDeadlineSeconds": 600,
//       "replicas": 1,
//       "revisionHistoryLimit": 1,
//       "selector": {
//         "matchExpressions": null,
//         "matchLabels": {
//           "app": "admin-ui",
//           "app.kubernetes.io/instance": "sentinel-dev-admin-ui",
//           "app.kubernetes.io/name": "admin-ui",
//           "branch": "master",
//           "version": "1.0.0"
//         }
//       },
//       "strategy": {
//         "rollingUpdate": {
//           "maxSurge": {
//             "value": "25%"
//           },
//           "maxUnavailable": {
//             "value": "25%"
//           }
//         },
//         "type": "RollingUpdate"
//       },
//       "template": {
//         "metadata": {
//           "annotations": {
//             "azure-pipelines/jobName": "deploy Web App",
//             "azure-pipelines/org": "https://bupaaunz.visualstudio.com/",
//             "azure-pipelines/pipeline": "Sentinel.UI.Admin",
//             "azure-pipelines/pipelineId": "3151",
//             "azure-pipelines/project": "AKS_Experiment",
//             "azure-pipelines/run": "20200907.2",
//             "azure-pipelines/runuri": "https://bupaaunz.visualstudio.com/AKS_Experiment/_build/results?buildId=248366"
//           },
//           "clusterName": null,
//           "creationTimestamp": null,
//           "deletionGracePeriodSeconds": null,
//           "deletionTimestamp": null,
//           "finalizers": null,
//           "generateName": null,
//           "generation": null,
//           "labels": {
//             "app": "admin-ui",
//             "app.kubernetes.io/instance": "sentinel-dev-admin-ui",
//             "app.kubernetes.io/name": "admin-ui",
//             "branch": "master",
//             "version": "1.0.0"
//           },
//           "managedFields": null,
//           "name": null,
//           "namespaceProperty": null,
//           "ownerReferences": null,
//           "resourceVersion": null,
//           "selfLink": null,
//           "uid": null
//         },
//         "spec": {
//           "activeDeadlineSeconds": null,
//           "affinity": null,
//           "automountServiceAccountToken": null,



//           "containers": [
//             {
//               "args": null,
//               "command": null,
//               "env": [
//                 {
//                   "name": "buildnumber",
//                   "value": "unknown",
//                   "valueFrom": null
//                 },
//                 {
//                   "name": "branch",
//                   "value": "master",
//                   "valueFrom": null
//                 }
//               ],
//               "envFrom": null,
//               "image": "mmercan/sentinel-ui-admin:20200907.2",
//               "imagePullPolicy": "IfNotPresent",
//               "lifecycle": null,
//               "livenessProbe": {
//                 "exec": null,
//                 "failureThreshold": 3,
//                 "httpGet": {
//                   "host": null,
//                   "httpHeaders": null,
//                   "path": "/",
//                   "port": {
//                     "value": "http"
//                   },
//                   "scheme": "HTTP"
//                 },
//                 "initialDelaySeconds": null,
//                 "periodSeconds": 10,
//                 "successThreshold": 1,
//                 "tcpSocket": null,
//                 "timeoutSeconds": 1
//               },
//               "name": "admin-ui",
//               "ports": [
//                 {
//                   "containerPort": 80,
//                   "hostIP": null,
//                   "hostPort": null,
//                   "name": "http",
//                   "protocol": "TCP"
//                 }
//               ],
//               "readinessProbe": {
//                 "exec": null,
//                 "failureThreshold": 3,
//                 "httpGet": {
//                   "host": null,
//                   "httpHeaders": null,
//                   "path": "/",
//                   "port": {
//                     "value": "http"
//                   },
//                   "scheme": "HTTP"
//                 },
//                 "initialDelaySeconds": null,
//                 "periodSeconds": 10,
//                 "successThreshold": 1,
//                 "tcpSocket": null,
//                 "timeoutSeconds": 1
//               },
//               "resources": {
//                 "limits": null,
//                 "requests": null
//               },
//               "securityContext": null,
//               "startupProbe": null,
//               "stdin": null,
//               "stdinOnce": null,
//               "terminationMessagePath": "/dev/termination-log",
//               "terminationMessagePolicy": "File",
//               "tty": null,
//               "volumeDevices": null,
//               "volumeMounts": null,
//               "workingDir": null
//             }
//           ],



//           "dnsConfig": null,
//           "dnsPolicy": "ClusterFirst",
//           "enableServiceLinks": null,
//           "ephemeralContainers": null,
//           "hostAliases": null,
//           "hostIPC": null,
//           "hostNetwork": null,
//           "hostPID": null,
//           "hostname": null,
//           "imagePullSecrets": null,
//           "initContainers": null,
//           "nodeName": null,
//           "nodeSelector": null,
//           "overhead": null,
//           "preemptionPolicy": null,
//           "priority": null,
//           "priorityClassName": null,
//           "readinessGates": null,
//           "restartPolicy": "Always",
//           "runtimeClassName": null,
//           "schedulerName": "default-scheduler",
//           "securityContext": {
//             "fsGroup": null,
//             "fsGroupChangePolicy": null,
//             "runAsGroup": null,
//             "runAsNonRoot": null,
//             "runAsUser": null,
//             "seLinuxOptions": null,
//             "supplementalGroups": null,
//             "sysctls": null,
//             "windowsOptions": null
//           },
//           "serviceAccount": null,
//           "serviceAccountName": null,
//           "shareProcessNamespace": null,
//           "subdomain": null,
//           "terminationGracePeriodSeconds": 30,
//           "tolerations": null,
//           "topologySpreadConstraints": null,
//           "volumes": null
//         }
//       }
//     },
//     "status": {
//       "availableReplicas": 1,
//       "collisionCount": null,
//       "conditions": [
//         {
//           "lastTransitionTime": "2020-09-07T08:16:35Z",
//           "lastUpdateTime": "2020-09-07T08:16:56Z",
//           "message": "ReplicaSet \"sentinel-dev-admin-ui-66d7c97dd4\" has successfully progressed.",
//           "reason": "NewReplicaSetAvailable",
//           "status": "True",
//           "type": "Progressing"
//         },
//         {
//           "lastTransitionTime": "2020-09-08T23:17:28Z",
//           "lastUpdateTime": "2020-09-08T23:17:28Z",
//           "message": "Deployment has minimum availability.",
//           "reason": "MinimumReplicasAvailable",
//           "status": "True",
//           "type": "Available"
//         }
//       ],
//       "observedGeneration": 1,
//       "readyReplicas": 1,
//       "replicas": 1,
//       "unavailableReplicas": null,
//       "updatedReplicas": 1
//     }
//   },