// package: k8App
// file: K8sHealthcheck.proto

var K8sHealthcheck_pb = require("./K8sHealthcheck_pb");
var google_protobuf_empty_pb = require("google-protobuf/google/protobuf/empty_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var NamespaceService = (function () {
  function NamespaceService() {}
  NamespaceService.serviceName = "k8App.NamespaceService";
  return NamespaceService;
}());

NamespaceService.GetNamespaces = {
  methodName: "GetNamespaces",
  service: NamespaceService,
  requestStream: false,
  responseStream: false,
  requestType: google_protobuf_empty_pb.Empty,
  responseType: K8sHealthcheck_pb.NamespaceListReply
};

NamespaceService.GetDeployments = {
  methodName: "GetDeployments",
  service: NamespaceService,
  requestStream: false,
  responseStream: false,
  requestType: K8sHealthcheck_pb.GetDeploymentsRequest,
  responseType: K8sHealthcheck_pb.DeploymentListReply
};

exports.NamespaceService = NamespaceService;

function NamespaceServiceClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

NamespaceServiceClient.prototype.getNamespaces = function getNamespaces(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(NamespaceService.GetNamespaces, {
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

NamespaceServiceClient.prototype.getDeployments = function getDeployments(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(NamespaceService.GetDeployments, {
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

exports.NamespaceServiceClient = NamespaceServiceClient;

