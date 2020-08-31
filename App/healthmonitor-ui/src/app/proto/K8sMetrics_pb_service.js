// package: k8App
// file: K8sMetrics.proto

var K8sMetrics_pb = require("./K8sMetrics_pb");
var google_protobuf_empty_pb = require("google-protobuf/google/protobuf/empty_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var K8MetricService = (function () {
  function K8MetricService() {}
  K8MetricService.serviceName = "k8App.K8MetricService";
  return K8MetricService;
}());

K8MetricService.GetNodeMetric = {
  methodName: "GetNodeMetric",
  service: K8MetricService,
  requestStream: false,
  responseStream: false,
  requestType: google_protobuf_empty_pb.Empty,
  responseType: K8sMetrics_pb.NodeMetricListReply
};

exports.K8MetricService = K8MetricService;

function K8MetricServiceClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

K8MetricServiceClient.prototype.getNodeMetric = function getNodeMetric(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(K8MetricService.GetNodeMetric, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

exports.K8MetricServiceClient = K8MetricServiceClient;

