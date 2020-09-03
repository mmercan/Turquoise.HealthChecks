export class K8sFakeDb {
    public static events = [
        {
            'action': null,
            'apiVersion': null,
            'count': 265,
            'eventTime': null,
            'firstTimestamp': '2020-08-31T23:30:31Z',
            'involvedObject': {
                'apiVersion': 'v1',
                'fieldPath': 'spec.containers{billing-api}',
                'kind': 'Pod',
                'name': 'sentinel-dev-billing-api-7468d4d879-l2qpx',
                'namespaceProperty': 'sentinel-dev',
                'resourceVersion': '28023568',
                'uid': '40901ca9-0b38-4621-94a9-e09d63281d4d'
            },
            'kind': null,
            'lastTimestamp': '2020-09-01T11:40:01Z',
            'message': 'Liveness probe failed: Get http://10.244.16.11:15020/app-health/billing-api/livez: net/http: request canceled (Client.Timeout exceeded while awaiting headers)',
            'metadata': {
                'annotations': null,
                'clusterName': null,
                'creationTimestamp': '2020-08-31T23:30:31Z',
                'deletionGracePeriodSeconds': null,
                'deletionTimestamp': null,
                'finalizers': null,
                'generateName': null,
                'generation': null,
                'labels': null,
                'managedFields': null,
                'name': 'sentinel-dev-billing-api-7468d4d879-l2qpx.16307e3480b87d39',
                'namespaceProperty': 'sentinel-dev',
                'ownerReferences': null,
                'resourceVersion': '28168494',
                'selfLink': '/api/v1/namespaces/sentinel-dev/events/sentinel-dev-billing-api-7468d4d879-l2qpx.16307e3480b87d39',
                'uid': '5917f5d9-7b2c-4dca-b911-a41ef2f2bc64'
            },
            'reason': 'Unhealthy',
            'related': null,
            'reportingComponent': '',
            'reportingInstance': '',
            'series': null,
            'source': {
                'component': 'kubelet',
                'host': 'aks-agentpool-54824633-vmss000057'
            },
            'type': 'Warning'
        },
        {
            'action': null,
            'apiVersion': null,
            'count': 251,
            'eventTime': null,
            'firstTimestamp': '2020-08-31T23:30:35Z',
            'involvedObject': {
                'apiVersion': 'v1',
                'fieldPath': 'spec.containers{billing-api}',
                'kind': 'Pod',
                'name': 'sentinel-dev-billing-api-7468d4d879-l2qpx',
                'namespaceProperty': 'sentinel-dev',
                'resourceVersion': '28023568',
                'uid': '40901ca9-0b38-4621-94a9-e09d63281d4d'
            },
            'kind': null,
            'lastTimestamp': '2020-09-01T11:24:35Z',
            'message': 'Readiness probe failed: Get http://10.244.16.11:15020/app-health/billing-api/readyz: net/http: request canceled (Client.Timeout exceeded while awaiting headers)',
            'metadata': {
                'annotations': null,
                'clusterName': null,
                'creationTimestamp': '2020-08-31T23:30:35Z',
                'deletionGracePeriodSeconds': null,
                'deletionTimestamp': null,
                'finalizers': null,
                'generateName': null,
                'generation': null,
                'labels': null,
                'managedFields': null,
                'name': 'sentinel-dev-billing-api-7468d4d879-l2qpx.16307e35763e740c',
                'namespaceProperty': 'sentinel-dev',
                'ownerReferences': null,
                'resourceVersion': '28165499',
                'selfLink': '/api/v1/namespaces/sentinel-dev/events/sentinel-dev-billing-api-7468d4d879-l2qpx.16307e35763e740c',
                'uid': '67fe72b1-6ee3-43e6-8283-da2c1e71b214'
            },
            'reason': 'Unhealthy',
            'related': null,
            'reportingComponent': '',
            'reportingInstance': '',
            'series': null,
            'source': {
                'component': 'kubelet',
                'host': 'aks-agentpool-54824633-vmss000057'
            },
            'type': 'Warning'
        },
        {
            'action': null,
            'apiVersion': null,
            'count': 8,
            'eventTime': null,
            'firstTimestamp': '2020-08-31T23:20:05Z',
            'involvedObject': {
                'apiVersion': 'v1',
                'fieldPath': 'spec.containers{healthmonitoring-api}',
                'kind': 'Pod',
                'name': 'sentinel-dev-health-api-healthmonitoring-api-6d9fb66b7c-tjnlc',
                'namespaceProperty': 'sentinel-dev',
                'resourceVersion': '28023577',
                'uid': '4e006739-02fd-4698-a8d7-636f6c4f7103'
            },
            'kind': null,
            'lastTimestamp': '2020-09-01T11:00:35Z',
            'message': 'Liveness probe failed: Get http://10.244.16.12:15020/app-health/healthmonitoring-api/livez: net/http: request canceled (Client.Timeout exceeded while awaiting headers)',
            'metadata': {
                'annotations': null,
                'clusterName': null,
                'creationTimestamp': '2020-09-01T10:00:35Z',
                'deletionGracePeriodSeconds': null,
                'deletionTimestamp': null,
                'finalizers': null,
                'generateName': null,
                'generation': null,
                'labels': null,
                'managedFields': null,
                'name': 'sentinel-dev-health-api-healthmonitoring-api-6d9fb66b7c-tjnlc.16307da2a3c127f6',
                'namespaceProperty': 'sentinel-dev',
                'ownerReferences': null,
                'resourceVersion': '28160834',
                'selfLink': '/api/v1/namespaces/sentinel-dev/events/sentinel-dev-health-api-healthmonitoring-api-6d9fb66b7c-tjnlc.16307da2a3c127f6',
                'uid': 'b5d7aebb-4e82-4fe1-9727-9e4d51b0a06c'
            },
            'reason': 'Unhealthy',
            'related': null,
            'reportingComponent': '',
            'reportingInstance': '',
            'series': null,
            'source': {
                'component': 'kubelet',
                'host': 'aks-agentpool-54824633-vmss000057'
            },
            'type': 'Warning'
        },
        {
            'action': null,
            'apiVersion': null,
            'count': 8,
            'eventTime': null,
            'firstTimestamp': '2020-09-01T04:15:42Z',
            'involvedObject': {
                'apiVersion': 'v1',
                'fieldPath': 'spec.containers{healthmonitoring-api}',
                'kind': 'Pod',
                'name': 'sentinel-dev-health-api-healthmonitoring-api-6d9fb66b7c-tjnlc',
                'namespaceProperty': 'sentinel-dev',
                'resourceVersion': '28023577',
                'uid': '4e006739-02fd-4698-a8d7-636f6c4f7103'
            },
            'kind': null,
            'lastTimestamp': '2020-09-01T11:30:32Z',
            'message': 'Readiness probe failed: Get http://10.244.16.12:15020/app-health/healthmonitoring-api/readyz: net/http: request canceled (Client.Timeout exceeded while awaiting headers)',
            'metadata': {
                'annotations': null,
                'clusterName': null,
                'creationTimestamp': '2020-09-01T11:30:32Z',
                'deletionGracePeriodSeconds': null,
                'deletionTimestamp': null,
                'finalizers': null,
                'generateName': null,
                'generation': null,
                'labels': null,
                'managedFields': null,
                'name': 'sentinel-dev-health-api-healthmonitoring-api-6d9fb66b7c-tjnlc.16308dc4648bc9e3',
                'namespaceProperty': 'sentinel-dev',
                'ownerReferences': null,
                'resourceVersion': '28166655',
                'selfLink': '/api/v1/namespaces/sentinel-dev/events/sentinel-dev-health-api-healthmonitoring-api-6d9fb66b7c-tjnlc.16308dc4648bc9e3',
                'uid': '3a9795b9-6e79-42ce-99e1-7bac506cc236'
            },
            'reason': 'Unhealthy',
            'related': null,
            'reportingComponent': '',
            'reportingInstance': '',
            'series': null,
            'source': {
                'component': 'kubelet',
                'host': 'aks-agentpool-54824633-vmss000057'
            },
            'type': 'Warning'
        }
    ];


    public static services = [
        {
            "nameandNamespace": "cm-acme-http-solver-hkw58.sentinel-dev",
            "uid": "f25f4320-bc72-4054-91a5-b357cc566f73",
            "name": "cm-acme-http-solver-hkw58",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "certmanager.k8s.io/acme-http-domain",
                    "value": "3545959542"
                },
                {
                    "key": "certmanager.k8s.io/acme-http-token",
                    "value": "1119555235"
                },
                {
                    "key": "certmanager.k8s.io/acme-http01-solver",
                    "value": "true"
                }
            ],
            "creationTime": "2020-05-25T15:37:55Z",
            "labelSelector": [
                {
                    "key": "certmanager.k8s.io/acme-http-domain",
                    "value": "3545959542"
                },
                {
                    "key": "certmanager.k8s.io/acme-http-token",
                    "value": "1119555235"
                },
                {
                    "key": "certmanager.k8s.io/acme-http01-solver",
                    "value": "true"
                }
            ],
            "annotations": [
                {
                    "key": "auth.istio.io/8089",
                    "value": "NONE"
                }
            ],
            "type": "NodePort",
            "sessionAffinity": "None",
            "clusterIP": "10.0.160.164",
            "internalEndpoints": [
                "cm-acme-http-solver-hkw58.sentinel-dev:8089"
            ],
            "externalEndpoints": [],
            "ingressUrl": "http://kibana.dev.api.sentinel.mercan.io",
            "virtualServiceUrl": null,
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": null,
            "healthIsaliveAndWellSyncDateUTC": "0001-01-01T00:00:00Z"
        },
        {
            "nameandNamespace": "cm-acme-http-solver-tl9tz.sentinel-dev",
            "uid": "cdc162bc-d0da-429b-afcc-901bdde67352",
            "name": "cm-acme-http-solver-tl9tz",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "certmanager.k8s.io/acme-http-domain",
                    "value": "4014869828"
                },
                {
                    "key": "certmanager.k8s.io/acme-http-token",
                    "value": "852626918"
                },
                {
                    "key": "certmanager.k8s.io/acme-http01-solver",
                    "value": "true"
                }
            ],
            "creationTime": "2020-05-26T02:56:39Z",
            "labelSelector": [
                {
                    "key": "certmanager.k8s.io/acme-http-domain",
                    "value": "4014869828"
                },
                {
                    "key": "certmanager.k8s.io/acme-http-token",
                    "value": "852626918"
                },
                {
                    "key": "certmanager.k8s.io/acme-http01-solver",
                    "value": "true"
                }
            ],
            "annotations": [
                {
                    "key": "auth.istio.io/8089",
                    "value": "NONE"
                }
            ],
            "type": "NodePort",
            "sessionAffinity": "None",
            "clusterIP": "10.0.205.202",
            "internalEndpoints": [
                "cm-acme-http-solver-tl9tz.sentinel-dev:8089"
            ],
            "externalEndpoints": [],
            "ingressUrl": "http://util-seq.dev.api.sentinel.mercan.io",
            "virtualServiceUrl": null,
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": null,
            "healthIsaliveAndWellSyncDateUTC": "0001-01-01T00:00:00Z"
        },
        {
            "nameandNamespace": "dev-puller-commits-devops-puller-commits.sentinel-dev",
            "uid": "446401a0-5dd1-4acc-aba6-4b1bbcb4acdd",
            "name": "dev-puller-commits-devops-puller-commits",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "dev-puller-commits"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "devops-puller-commits"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.16.0"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "devops-puller-commits-2.0.0-20200622.2"
                }
            ],
            "creationTime": "2020-06-18T08:47:24Z",
            "labelSelector": [
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "dev-puller-commits"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "devops-puller-commits"
                }
            ],
            "annotations": [],
            "type": "ClusterIP",
            "sessionAffinity": "None",
            "clusterIP": "10.0.49.212",
            "internalEndpoints": [
                "dev-puller-commits-devops-puller-commits.sentinel-dev:80"
            ],
            "externalEndpoints": [],
            "ingressUrl": null,
            "virtualServiceUrl": null,
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": null,
            "healthIsaliveAndWellSyncDateUTC": "0001-01-01T00:00:00Z"
        },
        {
            "nameandNamespace": "sentinel-dev-admin-ui.sentinel-dev",
            "uid": "4b3e9077-f24e-4673-8814-8cb3e6f0c2d5",
            "name": "sentinel-dev-admin-ui",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app",
                    "value": "admin-ui"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-admin-ui"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "admin-ui"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.0.0"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "admin-ui-2.0.0-20200823.1"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "creationTime": "2020-05-25T15:39:29Z",
            "labelSelector": [
                {
                    "key": "app",
                    "value": "admin-ui"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-admin-ui"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "admin-ui"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "annotations": [
                {
                    "key": "healthcheck/crontab",
                    "value": "*/15 * * * *"
                },
                {
                    "key": "healthcheck/isalive",
                    "value": "/Health/IsAlive"
                },
                {
                    "key": "healthcheck/isaliveandwell",
                    "value": "/Health/IsAliveAndWell"
                }
            ],
            "type": "ClusterIP",
            "sessionAffinity": "None",
            "clusterIP": "10.0.1.169",
            "internalEndpoints": [
                "sentinel-dev-admin-ui.sentinel-dev:80"
            ],
            "externalEndpoints": [],
            "ingressUrl": "https://admin-ui.dev.ui.sentinel.mercan.io",
            "virtualServiceUrl": null,
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": "OK",
            "healthIsaliveAndWellSyncDateUTC": "2020-08-28T12:30:41.35Z"
        },
        {
            "nameandNamespace": "sentinel-dev-billing-api.sentinel-dev",
            "uid": "b129a8a9-b3ef-4961-88c0-3a20d9ad1d0b",
            "name": "sentinel-dev-billing-api",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app",
                    "value": "billing-api"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-billing-api"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "billing-api"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.0"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "billing-api-2.0.0-20200824.1"
                },
                {
                    "key": "version",
                    "value": "1.0"
                }
            ],
            "creationTime": "2020-05-25T15:23:31Z",
            "labelSelector": [
                {
                    "key": "app",
                    "value": "billing-api"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-billing-api"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "billing-api"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "version",
                    "value": "1.0"
                }
            ],
            "annotations": [
                {
                    "key": "healthcheck/crontab",
                    "value": "*/15 * * * *"
                },
                {
                    "key": "healthcheck/isalive",
                    "value": "/Health/IsAlive"
                },
                {
                    "key": "healthcheck/isaliveandwell",
                    "value": "/Health/IsAliveAndWell"
                }
            ],
            "type": "ClusterIP",
            "sessionAffinity": "None",
            "clusterIP": "10.0.22.163",
            "internalEndpoints": [
                "sentinel-dev-billing-api.sentinel-dev:80"
            ],
            "externalEndpoints": [],
            "ingressUrl": null,
            "virtualServiceUrl": "http://billing-api.dev.api.sentinel.mercan.io",
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": "Healthy",
            "healthIsaliveAndWellSyncDateUTC": "2020-08-28T12:30:43.833Z"
        },
        {
            "nameandNamespace": "sentinel-dev-comms-api.sentinel-dev",
            "uid": "98a7eb83-f78f-4f01-a37a-9afe55ec544c",
            "name": "sentinel-dev-comms-api",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app",
                    "value": "comms-api"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-comms-api"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "comms-api"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.0"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "comms-api-2.0.0-20200824.2"
                },
                {
                    "key": "version",
                    "value": "1.0"
                }
            ],
            "creationTime": "2020-05-25T15:36:10Z",
            "labelSelector": [
                {
                    "key": "app",
                    "value": "comms-api"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-comms-api"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "comms-api"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "version",
                    "value": "1.0"
                }
            ],
            "annotations": [
                {
                    "key": "healthcheck/clientid",
                    "value": "67d009b1-97fe-4963-84ff-3590b06df0da"
                },
                {
                    "key": "healthcheck/crontab",
                    "value": "*/2 * * * *"
                },
                {
                    "key": "healthcheck/isalive",
                    "value": "/Health/IsAlive"
                },
                {
                    "key": "healthcheck/isaliveandwell",
                    "value": "/Health/IsAliveAndWell"
                }
            ],
            "type": "ClusterIP",
            "sessionAffinity": "None",
            "clusterIP": "10.0.73.4",
            "internalEndpoints": [
                "sentinel-dev-comms-api.sentinel-dev:80"
            ],
            "externalEndpoints": [],
            "ingressUrl": null,
            "virtualServiceUrl": "http://comms-api.dev.api.sentinel.mercan.io",
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": "Unhealthy",
            "healthIsaliveAndWellSyncDateUTC": "2020-08-28T12:32:49.22Z"
        },
        {
            "nameandNamespace": "sentinel-dev-db-elasticsearch.sentinel-dev",
            "uid": "bee0a278-cf87-4b8d-bb4b-557fc98ce721",
            "name": "sentinel-dev-db-elasticsearch",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app",
                    "value": "db-elasticsearch"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-db-elasticsearch"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "db-elasticsearch"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.0.0"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "db-elasticsearch-2.0.0-20200526.2"
                },
                {
                    "key": "service",
                    "value": "sentinel-dev-db-elasticsearch"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "creationTime": "2020-05-25T15:28:18Z",
            "labelSelector": [
                {
                    "key": "app",
                    "value": "db-elasticsearch"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-db-elasticsearch"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "db-elasticsearch"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "annotations": [],
            "type": "ClusterIP",
            "sessionAffinity": "None",
            "clusterIP": "10.0.222.86",
            "internalEndpoints": [
                "sentinel-dev-db-elasticsearch.sentinel-dev:80",
                "sentinel-dev-db-elasticsearch.sentinel-dev:9300"
            ],
            "externalEndpoints": [],
            "ingressUrl": null,
            "virtualServiceUrl": "http://db-elasticsearch.dev.api.sentinel.mercan.io",
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": null,
            "healthIsaliveAndWellSyncDateUTC": "0001-01-01T00:00:00Z"
        },
        {
            "nameandNamespace": "sentinel-dev-db-mongodb.sentinel-dev",
            "uid": "3726e7f3-f799-4677-a91e-ad65227e9ea3",
            "name": "sentinel-dev-db-mongodb",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-db-mongodb"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "db-mongodb"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "db-mongodb-2.0.0-20200804.1"
                }
            ],
            "creationTime": "2020-05-25T15:32:03Z",
            "labelSelector": [
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-db-mongodb"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "db-mongodb"
                }
            ],
            "annotations": [],
            "type": "LoadBalancer",
            "sessionAffinity": "None",
            "clusterIP": "10.0.48.33",
            "internalEndpoints": [
                "sentinel-dev-db-mongodb.sentinel-dev:27017"
            ],
            "externalEndpoints": [
                "52.184.215.67:27017"
            ],
            "ingressUrl": null,
            "virtualServiceUrl": null,
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": null,
            "healthIsaliveAndWellSyncDateUTC": "0001-01-01T00:00:00Z"
        },
        {
            "nameandNamespace": "sentinel-dev-db-redis.sentinel-dev",
            "uid": "8385edf5-56ee-4728-a96d-5d1e48f925d3",
            "name": "sentinel-dev-db-redis",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-db-redis"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "db-redis"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "db-redis-2.0.0-20200526.2"
                }
            ],
            "creationTime": "2020-05-25T15:34:45Z",
            "labelSelector": [
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-db-redis"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "db-redis"
                }
            ],
            "annotations": [],
            "type": "LoadBalancer",
            "sessionAffinity": "None",
            "clusterIP": "10.0.189.97",
            "internalEndpoints": [
                "sentinel-dev-db-redis.sentinel-dev:6379"
            ],
            "externalEndpoints": [
                "52.184.215.142:6379"
            ],
            "ingressUrl": null,
            "virtualServiceUrl": null,
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": null,
            "healthIsaliveAndWellSyncDateUTC": "0001-01-01T00:00:00Z"
        },
        {
            "nameandNamespace": "sentinel-dev-db-sql.sentinel-dev",
            "uid": "19c6f27e-70fa-4615-9fce-efa6bbd424f3",
            "name": "sentinel-dev-db-sql",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-db-sql"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "db-sql"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "db-sql-2.0.0-20200526.2"
                }
            ],
            "creationTime": "2020-05-25T15:35:52Z",
            "labelSelector": [
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-db-sql"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "db-sql"
                }
            ],
            "annotations": [],
            "type": "LoadBalancer",
            "sessionAffinity": "None",
            "clusterIP": "10.0.81.186",
            "internalEndpoints": [
                "sentinel-dev-db-sql.sentinel-dev:1433"
            ],
            "externalEndpoints": [
                "52.184.215.202:1433"
            ],
            "ingressUrl": null,
            "virtualServiceUrl": null,
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": null,
            "healthIsaliveAndWellSyncDateUTC": "0001-01-01T00:00:00Z"
        },
        {
            "nameandNamespace": "sentinel-dev-health-api-healthmonitoring-api.sentinel-dev",
            "uid": "37128348-30a3-4b3e-a084-6050c0abe08a",
            "name": "sentinel-dev-health-api-healthmonitoring-api",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app",
                    "value": "healthmonitoring-api"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-health-api"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "healthmonitoring-api"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.0"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "healthmonitoring-api-2.0.0-20200824.2"
                },
                {
                    "key": "version",
                    "value": "1.0"
                }
            ],
            "creationTime": "2020-05-25T14:37:56Z",
            "labelSelector": [
                {
                    "key": "app",
                    "value": "healthmonitoring-api"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-health-api"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "healthmonitoring-api"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "version",
                    "value": "1.0"
                }
            ],
            "annotations": [
                {
                    "key": "healthcheck/clientid",
                    "value": "67d009b1-97fe-4963-84ff-3590b06df0da"
                },
                {
                    "key": "healthcheck/crontab",
                    "value": "*/15 * * * *"
                },
                {
                    "key": "healthcheck/isalive",
                    "value": "/Health/IsAlive"
                },
                {
                    "key": "healthcheck/isaliveandwell",
                    "value": "/Health/IsAliveAndWell"
                }
            ],
            "type": "ClusterIP",
            "sessionAffinity": "None",
            "clusterIP": "10.0.76.41",
            "internalEndpoints": [
                "sentinel-dev-health-api-healthmonitoring-api.sentinel-dev:80"
            ],
            "externalEndpoints": [],
            "ingressUrl": null,
            "virtualServiceUrl": "http://healthmonitoring-api.dev.api.sentinel.mercan.io",
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": "Degraded",
            "healthIsaliveAndWellSyncDateUTC": "2020-08-28T12:30:44.101Z"
        },
        {
            "nameandNamespace": "sentinel-dev-health-ui-app-health-ui.sentinel-dev",
            "uid": "42aa8d0b-aff8-485c-8813-a273500aba0a",
            "name": "sentinel-dev-health-ui-app-health-ui",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app",
                    "value": "app-health-ui"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-health-ui"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "app-health-ui"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.0.0"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "app-health-ui-2.0.0-20200804.1"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "creationTime": "2020-05-25T15:39:46Z",
            "labelSelector": [
                {
                    "key": "app",
                    "value": "app-health-ui"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-health-ui"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "app-health-ui"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "annotations": [],
            "type": "ClusterIP",
            "sessionAffinity": "None",
            "clusterIP": "10.0.34.10",
            "internalEndpoints": [
                "sentinel-dev-health-ui-app-health-ui.sentinel-dev:80"
            ],
            "externalEndpoints": [],
            "ingressUrl": "https://health.dev.ui.sentinel.mercan.io",
            "virtualServiceUrl": null,
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": null,
            "healthIsaliveAndWellSyncDateUTC": "0001-01-01T00:00:00Z"
        },
        {
            "nameandNamespace": "sentinel-dev-member-api.sentinel-dev",
            "uid": "d69beb42-f784-473f-9e8f-9a08717eea96",
            "name": "sentinel-dev-member-api",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app",
                    "value": "member-api"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-member-api"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "member-api"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.0"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "member-api-2.0.0-20200823.2"
                },
                {
                    "key": "version",
                    "value": "1.0"
                }
            ],
            "creationTime": "2020-05-25T15:34:51Z",
            "labelSelector": [
                {
                    "key": "app",
                    "value": "member-api"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-member-api"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "member-api"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "version",
                    "value": "1.0"
                }
            ],
            "annotations": [
                {
                    "key": "healthcheck/clientid",
                    "value": "67d009b1-97fe-4963-84ff-3590b06df0da"
                },
                {
                    "key": "healthcheck/crontab",
                    "value": "*/15 * * * *"
                },
                {
                    "key": "healthcheck/isalive",
                    "value": "/Health/IsAlive"
                },
                {
                    "key": "healthcheck/isaliveandwell",
                    "value": "/Health/IsAliveAndWell"
                }
            ],
            "type": "ClusterIP",
            "sessionAffinity": "None",
            "clusterIP": "10.0.221.238",
            "internalEndpoints": [
                "sentinel-dev-member-api.sentinel-dev:80"
            ],
            "externalEndpoints": [],
            "ingressUrl": null,
            "virtualServiceUrl": "http://member-api.dev.api.sentinel.mercan.io",
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": "Healthy",
            "healthIsaliveAndWellSyncDateUTC": "2020-08-28T12:30:41.636Z"
        },
        {
            "nameandNamespace": "sentinel-dev-product-api.sentinel-dev",
            "uid": "0470ed18-b253-43e2-84f7-c66c98485a16",
            "name": "sentinel-dev-product-api",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app",
                    "value": "product-api"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-product-api"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "product-api"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.0"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "product-api-2.0.0-20200804.1"
                },
                {
                    "key": "version",
                    "value": "1.0"
                }
            ],
            "creationTime": "2020-05-26T01:13:20Z",
            "labelSelector": [
                {
                    "key": "app",
                    "value": "product-api"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-product-api"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "product-api"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "version",
                    "value": "1.0"
                }
            ],
            "annotations": [
                {
                    "key": "healthcheck/crontab",
                    "value": "*/15 * * * *"
                },
                {
                    "key": "healthcheck/isalive",
                    "value": "/healthcheck/isalive"
                },
                {
                    "key": "healthcheck/isaliveandwell",
                    "value": "/healthcheck/isaliveandwell"
                }
            ],
            "type": "ClusterIP",
            "sessionAffinity": "None",
            "clusterIP": "10.0.147.199",
            "internalEndpoints": [
                "sentinel-dev-product-api.sentinel-dev:80"
            ],
            "externalEndpoints": [],
            "ingressUrl": null,
            "virtualServiceUrl": "http://product-api.dev.api.sentinel.mercan.io",
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": "NotFound",
            "healthIsaliveAndWellSyncDateUTC": "2020-08-28T12:30:41.631Z"
        },
        {
            "nameandNamespace": "sentinel-dev-seq-util-seq.sentinel-dev",
            "uid": "7e70ebfe-4351-42d4-9f29-a31739c8a5aa",
            "name": "sentinel-dev-seq-util-seq",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app",
                    "value": "util-seq"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-seq"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "util-seq"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.0.0"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "util-seq-2.0.0-20200526.3"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "creationTime": "2020-05-26T02:50:20Z",
            "labelSelector": [
                {
                    "key": "app",
                    "value": "util-seq"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-seq"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "util-seq"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "annotations": [],
            "type": "ClusterIP",
            "sessionAffinity": "None",
            "clusterIP": "10.0.173.110",
            "internalEndpoints": [
                "sentinel-dev-seq-util-seq.sentinel-dev:80"
            ],
            "externalEndpoints": [],
            "ingressUrl": "https://util-seq.dev.ui.sentinel.mercan.io",
            "virtualServiceUrl": null,
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": null,
            "healthIsaliveAndWellSyncDateUTC": "0001-01-01T00:00:00Z"
        },
        {
            "nameandNamespace": "sentinel-dev-service-rabbitmq-http.sentinel-dev",
            "uid": "14eb2a5c-7357-4f46-bad7-5c98c6329c4f",
            "name": "sentinel-dev-service-rabbitmq-http",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app",
                    "value": "service-rabbitmq"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-service-rabbitmq"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "service-rabbitmq"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.0.0"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "service-rabbitmq-2.0.0-20200804.1"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "creationTime": "2020-05-25T15:36:53Z",
            "labelSelector": [
                {
                    "key": "app",
                    "value": "service-rabbitmq"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-service-rabbitmq"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "service-rabbitmq"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "annotations": [],
            "type": "ClusterIP",
            "sessionAffinity": "None",
            "clusterIP": "10.0.216.114",
            "internalEndpoints": [
                "sentinel-dev-service-rabbitmq-http.sentinel-dev:80"
            ],
            "externalEndpoints": [],
            "ingressUrl": null,
            "virtualServiceUrl": "http://service-rabbitmq.dev.api.sentinel.mercan.io",
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": null,
            "healthIsaliveAndWellSyncDateUTC": "0001-01-01T00:00:00Z"
        },
        {
            "nameandNamespace": "sentinel-dev-service-rabbitmq-tcp.sentinel-dev",
            "uid": "f044d303-7acd-4672-b608-2c380a64ba9d",
            "name": "sentinel-dev-service-rabbitmq-tcp",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app",
                    "value": "service-rabbitmq"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-service-rabbitmq"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "service-rabbitmq"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.0.0"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "service-rabbitmq-2.0.0-20200804.1"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "creationTime": "2020-05-25T15:36:53Z",
            "labelSelector": [
                {
                    "key": "app",
                    "value": "service-rabbitmq"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-service-rabbitmq"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "service-rabbitmq"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "annotations": [],
            "type": "LoadBalancer",
            "sessionAffinity": "None",
            "clusterIP": "10.0.213.122",
            "internalEndpoints": [
                "sentinel-dev-service-rabbitmq-tcp.sentinel-dev:5672"
            ],
            "externalEndpoints": [
                "52.184.215.190:5672"
            ],
            "ingressUrl": null,
            "virtualServiceUrl": null,
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": null,
            "healthIsaliveAndWellSyncDateUTC": "0001-01-01T00:00:00Z"
        },
        {
            "nameandNamespace": "sentinel-dev-util-kibana.sentinel-dev",
            "uid": "1ad37c4a-4a10-49e6-9e3b-792b23dc0dfb",
            "name": "sentinel-dev-util-kibana",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app",
                    "value": "util-kibana"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-util-kibana"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "util-kibana"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.0.0"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "util-kibana-2.0.0-20200526.2"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "creationTime": "2020-05-25T15:37:52Z",
            "labelSelector": [
                {
                    "key": "app",
                    "value": "util-kibana"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-dev-util-kibana"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "util-kibana"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "annotations": [],
            "type": "ClusterIP",
            "sessionAffinity": "None",
            "clusterIP": "10.0.98.73",
            "internalEndpoints": [
                "sentinel-dev-util-kibana.sentinel-dev:80"
            ],
            "externalEndpoints": [],
            "ingressUrl": "https://kibana.dev.api.sentinel.mercan.io",
            "virtualServiceUrl": "http://util-kibana.dev.api.sentinel.mercan.io",
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": null,
            "healthIsaliveAndWellSyncDateUTC": "0001-01-01T00:00:00Z"
        },
        {
            "nameandNamespace": "sentinel-util-mailhog.sentinel-dev",
            "uid": "36bab11c-44ab-41bc-8164-c38609419acb",
            "name": "sentinel-util-mailhog",
            "namespace": "sentinel-dev",
            "labels": [
                {
                    "key": "app",
                    "value": "util-mailhog"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-util-mailhog"
                },
                {
                    "key": "app.kubernetes.io/managed-by",
                    "value": "Helm"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "util-mailhog"
                },
                {
                    "key": "app.kubernetes.io/version",
                    "value": "1.0.0"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "helm.sh/chart",
                    "value": "util-mailhog-2.0.0-20200526.2"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "creationTime": "2020-05-25T15:38:29Z",
            "labelSelector": [
                {
                    "key": "app",
                    "value": "util-mailhog"
                },
                {
                    "key": "app.kubernetes.io/instance",
                    "value": "sentinel-util-mailhog"
                },
                {
                    "key": "app.kubernetes.io/name",
                    "value": "util-mailhog"
                },
                {
                    "key": "branch",
                    "value": "master"
                },
                {
                    "key": "version",
                    "value": "1.0.0"
                }
            ],
            "annotations": [],
            "type": "ClusterIP",
            "sessionAffinity": "None",
            "clusterIP": "10.0.116.129",
            "internalEndpoints": [
                "sentinel-util-mailhog.sentinel-dev:80"
            ],
            "externalEndpoints": [],
            "ingressUrl": null,
            "virtualServiceUrl": "http://util-mailhog.dev.api.sentinel.mercan.io",
            "latestSyncDateUTC": "2020-08-28T12:30:02.289Z",
            "deleted": false,
            "healthIsalive": null,
            "healthIsaliveSyncDateUTC": "0001-01-01T00:00:00Z",
            "healthIsaliveAndWell": null,
            "healthIsaliveAndWellSyncDateUTC": "0001-01-01T00:00:00Z"
        }
    ]
}
