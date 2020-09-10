using System.Collections.Generic;

namespace Turquoise.Models.Mongo
{
    public class ContainerV1
    {
        public ProbeV1 LivenessProbe { get; set; }
        public ProbeV1 ReadinessProbe { get; set; }
        public ProbeV1 StartupProbe { get; set; }

        public string StartupProbeV1 { get; set; }
        public string ReadinessProbeV1 { get; set; }
        public string LivenessProbeV1 { get; set; }

        public IList<ContainerPortV1> Ports { get; set; }
        public string Name { get; set; }
        public string ImagePullPolicy { get; set; }
        public string Image { get; set; }
        public string ConfigMapRef { get; set; }
        public string SecretMapRef { get; set; }
        public IList<string> Command { get; set; }
        public IList<string> Args { get; set; }


        public string WorkingDir { get; set; }


        // public string Image { get; set; }



        //       public IList<V1VolumeMount> VolumeMounts { get; set; }

        //  public IList<V1VolumeDevice> VolumeDevices { get; set; }
        //    public bool? Tty { get; set; }
        //     public string TerminationMessagePolicy { get; set; }
        //    public string TerminationMessagePath { get; set; }
        //      public bool? StdinOnce { get; set; }
        //      public bool? Stdin { get; set; }

    }



    public class ContainerPortV1
    {

        public int ContainerPort { get; set; }

        public string HostIP { get; set; }

        public int? HostPort { get; set; }
        public string Name { get; set; }
        public string Protocol { get; set; }

    }

}


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