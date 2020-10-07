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

NamespaceService.GetServices = {
  methodName: "GetServices",
  service: NamespaceService,
  requestStream: false,
  responseStream: false,
  requestType: K8sHealthcheck_pb.GetServicesRequest,
  responseType: K8sHealthcheck_pb.ServiceListReply
};

NamespaceService.GetService = {
  methodName: "GetService",
  service: NamespaceService,
  requestStream: false,
  responseStream: false,
  requestType: K8sHealthcheck_pb.GetServiceRequest,
  responseType: K8sHealthcheck_pb.ServiceReply
};

NamespaceService.GetDeployments = {
  methodName: "GetDeployments",
  service: NamespaceService,
  requestStream: false,
  responseStream: false,
  requestType: K8sHealthcheck_pb.GetDeploymentsRequest,
  responseType: K8sHealthcheck_pb.DeploymentListReply
};

NamespaceService.GetEvents = {
  methodName: "GetEvents",
  service: NamespaceService,
  requestStream: false,
  responseStream: false,
  requestType: K8sHealthcheck_pb.GetEventListRequest,
  responseType: K8sHealthcheck_pb.EventListReply
};

NamespaceService.GetHealthCheckStats = {
  methodName: "GetHealthCheckStats",
  service: NamespaceService,
  requestStream: false,
  responseStream: false,
  requestType: K8sHealthcheck_pb.HealthCheckStatsRequest,
  responseType: K8sHealthcheck_pb.HealthCheckStatsReply
};

NamespaceService.GetLastHealthCheckResult = {
  methodName: "GetLastHealthCheckResult",
  service: NamespaceService,
  requestStream: false,
  responseStream: false,
  requestType: K8sHealthcheck_pb.HealthCheckResultRequest,
  responseType: K8sHealthcheck_pb.HealthCheckResultReply
};

NamespaceService.GetNodes = {
  methodName: "GetNodes",
  service: NamespaceService,
  requestStream: false,
  responseStream: false,
  requestType: google_protobuf_empty_pb.Empty,
  responseType: K8sHealthcheck_pb.NodeReplies
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

NamespaceServiceClient.prototype.getServices = function getServices(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(NamespaceService.GetServices, {
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

NamespaceServiceClient.prototype.getService = function getService(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(NamespaceService.GetService, {
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

NamespaceServiceClient.prototype.getEvents = function getEvents(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(NamespaceService.GetEvents, {
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

NamespaceServiceClient.prototype.getHealthCheckStats = function getHealthCheckStats(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(NamespaceService.GetHealthCheckStats, {
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

NamespaceServiceClient.prototype.getLastHealthCheckResult = function getLastHealthCheckResult(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(NamespaceService.GetLastHealthCheckResult, {
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

NamespaceServiceClient.prototype.getNodes = function getNodes(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(NamespaceService.GetNodes, {
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

