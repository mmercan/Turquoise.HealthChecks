// package: k8App
// file: K8sHealthcheck.proto

import * as K8sHealthcheck_pb from "./K8sHealthcheck_pb";
import * as google_protobuf_empty_pb from "google-protobuf/google/protobuf/empty_pb";
import {grpc} from "@improbable-eng/grpc-web";

type NamespaceServiceGetNamespaces = {
  readonly methodName: string;
  readonly service: typeof NamespaceService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof google_protobuf_empty_pb.Empty;
  readonly responseType: typeof K8sHealthcheck_pb.NamespaceListReply;
};

type NamespaceServiceGetServices = {
  readonly methodName: string;
  readonly service: typeof NamespaceService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof K8sHealthcheck_pb.GetServicesRequest;
  readonly responseType: typeof K8sHealthcheck_pb.ServiceListReply;
};

type NamespaceServiceGetDeployments = {
  readonly methodName: string;
  readonly service: typeof NamespaceService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof K8sHealthcheck_pb.GetDeploymentsRequest;
  readonly responseType: typeof K8sHealthcheck_pb.DeploymentListReply;
};

type NamespaceServiceGetEvents = {
  readonly methodName: string;
  readonly service: typeof NamespaceService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof K8sHealthcheck_pb.GetEventListRequest;
  readonly responseType: typeof K8sHealthcheck_pb.EventListReply;
};

type NamespaceServiceGetHealthCheckStats = {
  readonly methodName: string;
  readonly service: typeof NamespaceService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof K8sHealthcheck_pb.HealthCheckStatsRequest;
  readonly responseType: typeof K8sHealthcheck_pb.HealthCheckStatsReply;
};

export class NamespaceService {
  static readonly serviceName: string;
  static readonly GetNamespaces: NamespaceServiceGetNamespaces;
  static readonly GetServices: NamespaceServiceGetServices;
  static readonly GetDeployments: NamespaceServiceGetDeployments;
  static readonly GetEvents: NamespaceServiceGetEvents;
  static readonly GetHealthCheckStats: NamespaceServiceGetHealthCheckStats;
}

export type ServiceError = { message: string, code: number; metadata: grpc.Metadata }
export type Status = { details: string, code: number; metadata: grpc.Metadata }

interface UnaryResponse {
  cancel(): void;
}
interface ResponseStream<T> {
  cancel(): void;
  on(type: 'data', handler: (message: T) => void): ResponseStream<T>;
  on(type: 'end', handler: (status?: Status) => void): ResponseStream<T>;
  on(type: 'status', handler: (status: Status) => void): ResponseStream<T>;
}
interface RequestStream<T> {
  write(message: T): RequestStream<T>;
  end(): void;
  cancel(): void;
  on(type: 'end', handler: (status?: Status) => void): RequestStream<T>;
  on(type: 'status', handler: (status: Status) => void): RequestStream<T>;
}
interface BidirectionalStream<ReqT, ResT> {
  write(message: ReqT): BidirectionalStream<ReqT, ResT>;
  end(): void;
  cancel(): void;
  on(type: 'data', handler: (message: ResT) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'end', handler: (status?: Status) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'status', handler: (status: Status) => void): BidirectionalStream<ReqT, ResT>;
}

export class NamespaceServiceClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  getNamespaces(
    requestMessage: google_protobuf_empty_pb.Empty,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: K8sHealthcheck_pb.NamespaceListReply|null) => void
  ): UnaryResponse;
  getNamespaces(
    requestMessage: google_protobuf_empty_pb.Empty,
    callback: (error: ServiceError|null, responseMessage: K8sHealthcheck_pb.NamespaceListReply|null) => void
  ): UnaryResponse;
  getServices(
    requestMessage: K8sHealthcheck_pb.GetServicesRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: K8sHealthcheck_pb.ServiceListReply|null) => void
  ): UnaryResponse;
  getServices(
    requestMessage: K8sHealthcheck_pb.GetServicesRequest,
    callback: (error: ServiceError|null, responseMessage: K8sHealthcheck_pb.ServiceListReply|null) => void
  ): UnaryResponse;
  getDeployments(
    requestMessage: K8sHealthcheck_pb.GetDeploymentsRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: K8sHealthcheck_pb.DeploymentListReply|null) => void
  ): UnaryResponse;
  getDeployments(
    requestMessage: K8sHealthcheck_pb.GetDeploymentsRequest,
    callback: (error: ServiceError|null, responseMessage: K8sHealthcheck_pb.DeploymentListReply|null) => void
  ): UnaryResponse;
  getEvents(
    requestMessage: K8sHealthcheck_pb.GetEventListRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: K8sHealthcheck_pb.EventListReply|null) => void
  ): UnaryResponse;
  getEvents(
    requestMessage: K8sHealthcheck_pb.GetEventListRequest,
    callback: (error: ServiceError|null, responseMessage: K8sHealthcheck_pb.EventListReply|null) => void
  ): UnaryResponse;
  getHealthCheckStats(
    requestMessage: K8sHealthcheck_pb.HealthCheckStatsRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: K8sHealthcheck_pb.HealthCheckStatsReply|null) => void
  ): UnaryResponse;
  getHealthCheckStats(
    requestMessage: K8sHealthcheck_pb.HealthCheckStatsRequest,
    callback: (error: ServiceError|null, responseMessage: K8sHealthcheck_pb.HealthCheckStatsReply|null) => void
  ): UnaryResponse;
}

