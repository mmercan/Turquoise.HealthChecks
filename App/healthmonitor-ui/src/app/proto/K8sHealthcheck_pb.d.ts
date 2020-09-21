// package: k8App
// file: K8sHealthcheck.proto

import * as jspb from "google-protobuf";
import * as google_protobuf_timestamp_pb from "google-protobuf/google/protobuf/timestamp_pb";
import * as google_protobuf_empty_pb from "google-protobuf/google/protobuf/empty_pb";

export class GetServicesRequest extends jspb.Message {
  getNamespaceparam(): string;
  setNamespaceparam(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetServicesRequest.AsObject;
  static toObject(includeInstance: boolean, msg: GetServicesRequest): GetServicesRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: GetServicesRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetServicesRequest;
  static deserializeBinaryFromReader(message: GetServicesRequest, reader: jspb.BinaryReader): GetServicesRequest;
}

export namespace GetServicesRequest {
  export type AsObject = {
    namespaceparam: string,
  }
}

export class GetServiceRequest extends jspb.Message {
  getNamespaceparam(): string;
  setNamespaceparam(value: string): void;

  getServicename(): string;
  setServicename(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetServiceRequest.AsObject;
  static toObject(includeInstance: boolean, msg: GetServiceRequest): GetServiceRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: GetServiceRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetServiceRequest;
  static deserializeBinaryFromReader(message: GetServiceRequest, reader: jspb.BinaryReader): GetServiceRequest;
}

export namespace GetServiceRequest {
  export type AsObject = {
    namespaceparam: string,
    servicename: string,
  }
}

export class GetDeploymentsRequest extends jspb.Message {
  getNamespace(): string;
  setNamespace(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetDeploymentsRequest.AsObject;
  static toObject(includeInstance: boolean, msg: GetDeploymentsRequest): GetDeploymentsRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: GetDeploymentsRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetDeploymentsRequest;
  static deserializeBinaryFromReader(message: GetDeploymentsRequest, reader: jspb.BinaryReader): GetDeploymentsRequest;
}

export namespace GetDeploymentsRequest {
  export type AsObject = {
    namespace: string,
  }
}

export class NamespaceListReply extends jspb.Message {
  clearNamespacesList(): void;
  getNamespacesList(): Array<NamespaceReply>;
  setNamespacesList(value: Array<NamespaceReply>): void;
  addNamespaces(value?: NamespaceReply, index?: number): NamespaceReply;

  hasUpdatedtime(): boolean;
  clearUpdatedtime(): void;
  getUpdatedtime(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setUpdatedtime(value?: google_protobuf_timestamp_pb.Timestamp): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): NamespaceListReply.AsObject;
  static toObject(includeInstance: boolean, msg: NamespaceListReply): NamespaceListReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: NamespaceListReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): NamespaceListReply;
  static deserializeBinaryFromReader(message: NamespaceListReply, reader: jspb.BinaryReader): NamespaceListReply;
}

export namespace NamespaceListReply {
  export type AsObject = {
    namespacesList: Array<NamespaceReply.AsObject>,
    updatedtime?: google_protobuf_timestamp_pb.Timestamp.AsObject,
  }
}

export class NamespaceReply extends jspb.Message {
  getNamespace(): string;
  setNamespace(value: string): void;

  getWarning(): number;
  setWarning(value: number): void;

  getErrors(): number;
  setErrors(value: number): void;

  hasCreationdate(): boolean;
  clearCreationdate(): void;
  getCreationdate(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setCreationdate(value?: google_protobuf_timestamp_pb.Timestamp): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): NamespaceReply.AsObject;
  static toObject(includeInstance: boolean, msg: NamespaceReply): NamespaceReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: NamespaceReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): NamespaceReply;
  static deserializeBinaryFromReader(message: NamespaceReply, reader: jspb.BinaryReader): NamespaceReply;
}

export namespace NamespaceReply {
  export type AsObject = {
    namespace: string,
    warning: number,
    errors: number,
    creationdate?: google_protobuf_timestamp_pb.Timestamp.AsObject,
  }
}

export class DeploymentListReply extends jspb.Message {
  clearDeploymentsList(): void;
  getDeploymentsList(): Array<DeploymentReply>;
  setDeploymentsList(value: Array<DeploymentReply>): void;
  addDeployments(value?: DeploymentReply, index?: number): DeploymentReply;

  hasUpdatedtime(): boolean;
  clearUpdatedtime(): void;
  getUpdatedtime(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setUpdatedtime(value?: google_protobuf_timestamp_pb.Timestamp): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): DeploymentListReply.AsObject;
  static toObject(includeInstance: boolean, msg: DeploymentListReply): DeploymentListReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: DeploymentListReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): DeploymentListReply;
  static deserializeBinaryFromReader(message: DeploymentListReply, reader: jspb.BinaryReader): DeploymentListReply;
}

export namespace DeploymentListReply {
  export type AsObject = {
    deploymentsList: Array<DeploymentReply.AsObject>,
    updatedtime?: google_protobuf_timestamp_pb.Timestamp.AsObject,
  }
}

export class DeploymentReply extends jspb.Message {
  getName(): string;
  setName(value: string): void;

  clearLabelsList(): void;
  getLabelsList(): Array<Pair>;
  setLabelsList(value: Array<Pair>): void;
  addLabels(value?: Pair, index?: number): Pair;

  getImage(): string;
  setImage(value: string): void;

  getStatus(): string;
  setStatus(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): DeploymentReply.AsObject;
  static toObject(includeInstance: boolean, msg: DeploymentReply): DeploymentReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: DeploymentReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): DeploymentReply;
  static deserializeBinaryFromReader(message: DeploymentReply, reader: jspb.BinaryReader): DeploymentReply;
}

export namespace DeploymentReply {
  export type AsObject = {
    name: string,
    labelsList: Array<Pair.AsObject>,
    image: string,
    status: string,
  }
}

export class Pair extends jspb.Message {
  getKey(): string;
  setKey(value: string): void;

  getValue(): string;
  setValue(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): Pair.AsObject;
  static toObject(includeInstance: boolean, msg: Pair): Pair.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: Pair, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): Pair;
  static deserializeBinaryFromReader(message: Pair, reader: jspb.BinaryReader): Pair;
}

export namespace Pair {
  export type AsObject = {
    key: string,
    value: string,
  }
}

export class StringMessage extends jspb.Message {
  getValue(): string;
  setValue(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): StringMessage.AsObject;
  static toObject(includeInstance: boolean, msg: StringMessage): StringMessage.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: StringMessage, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): StringMessage;
  static deserializeBinaryFromReader(message: StringMessage, reader: jspb.BinaryReader): StringMessage;
}

export namespace StringMessage {
  export type AsObject = {
    value: string,
  }
}

export class ServiceListReply extends jspb.Message {
  clearServicesList(): void;
  getServicesList(): Array<ServiceReply>;
  setServicesList(value: Array<ServiceReply>): void;
  addServices(value?: ServiceReply, index?: number): ServiceReply;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ServiceListReply.AsObject;
  static toObject(includeInstance: boolean, msg: ServiceListReply): ServiceListReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: ServiceListReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ServiceListReply;
  static deserializeBinaryFromReader(message: ServiceListReply, reader: jspb.BinaryReader): ServiceListReply;
}

export namespace ServiceListReply {
  export type AsObject = {
    servicesList: Array<ServiceReply.AsObject>,
  }
}

export class ServiceReply extends jspb.Message {
  getNameandnamespace(): string;
  setNameandnamespace(value: string): void;

  getUid(): string;
  setUid(value: string): void;

  getName(): string;
  setName(value: string): void;

  getNamespace(): string;
  setNamespace(value: string): void;

  clearLabelsList(): void;
  getLabelsList(): Array<Pair>;
  setLabelsList(value: Array<Pair>): void;
  addLabels(value?: Pair, index?: number): Pair;

  hasCreationtime(): boolean;
  clearCreationtime(): void;
  getCreationtime(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setCreationtime(value?: google_protobuf_timestamp_pb.Timestamp): void;

  clearLabelselectorList(): void;
  getLabelselectorList(): Array<Pair>;
  setLabelselectorList(value: Array<Pair>): void;
  addLabelselector(value?: Pair, index?: number): Pair;

  clearAnnotationsList(): void;
  getAnnotationsList(): Array<Pair>;
  setAnnotationsList(value: Array<Pair>): void;
  addAnnotations(value?: Pair, index?: number): Pair;

  getServicetype(): string;
  setServicetype(value: string): void;

  getSessionaffinity(): string;
  setSessionaffinity(value: string): void;

  getClusterip(): string;
  setClusterip(value: string): void;

  clearInternalendpointsList(): void;
  getInternalendpointsList(): Array<StringMessage>;
  setInternalendpointsList(value: Array<StringMessage>): void;
  addInternalendpoints(value?: StringMessage, index?: number): StringMessage;

  clearExternalendpointsList(): void;
  getExternalendpointsList(): Array<StringMessage>;
  setExternalendpointsList(value: Array<StringMessage>): void;
  addExternalendpoints(value?: StringMessage, index?: number): StringMessage;

  getIngressurl(): string;
  setIngressurl(value: string): void;

  getVirtualserviceurl(): string;
  setVirtualserviceurl(value: string): void;

  hasLatestsyncdateutc(): boolean;
  clearLatestsyncdateutc(): void;
  getLatestsyncdateutc(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setLatestsyncdateutc(value?: google_protobuf_timestamp_pb.Timestamp): void;

  getDeleted(): boolean;
  setDeleted(value: boolean): void;

  getHealthisalive(): string;
  setHealthisalive(value: string): void;

  hasHealthisalivesyncdateutc(): boolean;
  clearHealthisalivesyncdateutc(): void;
  getHealthisalivesyncdateutc(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setHealthisalivesyncdateutc(value?: google_protobuf_timestamp_pb.Timestamp): void;

  getHealthisaliveandwell(): string;
  setHealthisaliveandwell(value: string): void;

  hasHealthisaliveandwellsyncdateutc(): boolean;
  clearHealthisaliveandwellsyncdateutc(): void;
  getHealthisaliveandwellsyncdateutc(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setHealthisaliveandwellsyncdateutc(value?: google_protobuf_timestamp_pb.Timestamp): void;

  getLivenessprobe(): string;
  setLivenessprobe(value: string): void;

  getReadinessprobe(): string;
  setReadinessprobe(value: string): void;

  getStartupprobe(): string;
  setStartupprobe(value: string): void;

  getCrondescription(): string;
  setCrondescription(value: string): void;

  getCrontab(): string;
  setCrontab(value: string): void;

  getCrontabexception(): string;
  setCrontabexception(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ServiceReply.AsObject;
  static toObject(includeInstance: boolean, msg: ServiceReply): ServiceReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: ServiceReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ServiceReply;
  static deserializeBinaryFromReader(message: ServiceReply, reader: jspb.BinaryReader): ServiceReply;
}

export namespace ServiceReply {
  export type AsObject = {
    nameandnamespace: string,
    uid: string,
    name: string,
    namespace: string,
    labelsList: Array<Pair.AsObject>,
    creationtime?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    labelselectorList: Array<Pair.AsObject>,
    annotationsList: Array<Pair.AsObject>,
    servicetype: string,
    sessionaffinity: string,
    clusterip: string,
    internalendpointsList: Array<StringMessage.AsObject>,
    externalendpointsList: Array<StringMessage.AsObject>,
    ingressurl: string,
    virtualserviceurl: string,
    latestsyncdateutc?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    deleted: boolean,
    healthisalive: string,
    healthisalivesyncdateutc?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    healthisaliveandwell: string,
    healthisaliveandwellsyncdateutc?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    livenessprobe: string,
    readinessprobe: string,
    startupprobe: string,
    crondescription: string,
    crontab: string,
    crontabexception: string,
  }
}

export class GetEventListRequest extends jspb.Message {
  getNamespaceparam(): string;
  setNamespaceparam(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetEventListRequest.AsObject;
  static toObject(includeInstance: boolean, msg: GetEventListRequest): GetEventListRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: GetEventListRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetEventListRequest;
  static deserializeBinaryFromReader(message: GetEventListRequest, reader: jspb.BinaryReader): GetEventListRequest;
}

export namespace GetEventListRequest {
  export type AsObject = {
    namespaceparam: string,
  }
}

export class EventListReply extends jspb.Message {
  clearEventsList(): void;
  getEventsList(): Array<EventReply>;
  setEventsList(value: Array<EventReply>): void;
  addEvents(value?: EventReply, index?: number): EventReply;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): EventListReply.AsObject;
  static toObject(includeInstance: boolean, msg: EventListReply): EventListReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: EventListReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): EventListReply;
  static deserializeBinaryFromReader(message: EventListReply, reader: jspb.BinaryReader): EventListReply;
}

export namespace EventListReply {
  export type AsObject = {
    eventsList: Array<EventReply.AsObject>,
  }
}

export class EventReply extends jspb.Message {
  getName(): string;
  setName(value: string): void;

  getMessage(): string;
  setMessage(value: string): void;

  getCount(): number;
  setCount(value: number): void;

  getReason(): string;
  setReason(value: string): void;

  getType(): string;
  setType(value: string): void;

  hasFirsttimestamp(): boolean;
  clearFirsttimestamp(): void;
  getFirsttimestamp(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setFirsttimestamp(value?: google_protobuf_timestamp_pb.Timestamp): void;

  hasLasttimestamp(): boolean;
  clearLasttimestamp(): void;
  getLasttimestamp(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setLasttimestamp(value?: google_protobuf_timestamp_pb.Timestamp): void;

  getInvolvedobjectname(): string;
  setInvolvedobjectname(value: string): void;

  getInvolvedobjectkind(): string;
  setInvolvedobjectkind(value: string): void;

  getInvolvedobjectnamespace(): string;
  setInvolvedobjectnamespace(value: string): void;

  getInvolvedobjectuid(): string;
  setInvolvedobjectuid(value: string): void;

  clearMetadataList(): void;
  getMetadataList(): Array<Pair>;
  setMetadataList(value: Array<Pair>): void;
  addMetadata(value?: Pair, index?: number): Pair;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): EventReply.AsObject;
  static toObject(includeInstance: boolean, msg: EventReply): EventReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: EventReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): EventReply;
  static deserializeBinaryFromReader(message: EventReply, reader: jspb.BinaryReader): EventReply;
}

export namespace EventReply {
  export type AsObject = {
    name: string,
    message: string,
    count: number,
    reason: string,
    type: string,
    firsttimestamp?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    lasttimestamp?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    involvedobjectname: string,
    involvedobjectkind: string,
    involvedobjectnamespace: string,
    involvedobjectuid: string,
    metadataList: Array<Pair.AsObject>,
  }
}

export class HealthCheckStatsRequest extends jspb.Message {
  getNamespaceparam(): string;
  setNamespaceparam(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): HealthCheckStatsRequest.AsObject;
  static toObject(includeInstance: boolean, msg: HealthCheckStatsRequest): HealthCheckStatsRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: HealthCheckStatsRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): HealthCheckStatsRequest;
  static deserializeBinaryFromReader(message: HealthCheckStatsRequest, reader: jspb.BinaryReader): HealthCheckStatsRequest;
}

export namespace HealthCheckStatsRequest {
  export type AsObject = {
    namespaceparam: string,
  }
}

export class HealthCheckStatsReply extends jspb.Message {
  hasSyncdate(): boolean;
  clearSyncdate(): void;
  getSyncdate(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setSyncdate(value?: google_protobuf_timestamp_pb.Timestamp): void;

  getAllservices(): number;
  setAllservices(value: number): void;

  getAllrunsontoday(): number;
  setAllrunsontoday(value: number): void;

  getHealthyrunsontoday(): number;
  setHealthyrunsontoday(value: number): void;

  getUnhealthyrunsontoday(): number;
  setUnhealthyrunsontoday(value: number): void;

  clearUnhealthyservicestodayList(): void;
  getUnhealthyservicestodayList(): Array<StringMessage>;
  setUnhealthyservicestodayList(value: Array<StringMessage>): void;
  addUnhealthyservicestoday(value?: StringMessage, index?: number): StringMessage;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): HealthCheckStatsReply.AsObject;
  static toObject(includeInstance: boolean, msg: HealthCheckStatsReply): HealthCheckStatsReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: HealthCheckStatsReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): HealthCheckStatsReply;
  static deserializeBinaryFromReader(message: HealthCheckStatsReply, reader: jspb.BinaryReader): HealthCheckStatsReply;
}

export namespace HealthCheckStatsReply {
  export type AsObject = {
    syncdate?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    allservices: number,
    allrunsontoday: number,
    healthyrunsontoday: number,
    unhealthyrunsontoday: number,
    unhealthyservicestodayList: Array<StringMessage.AsObject>,
  }
}

export class HealthCheckResultRequest extends jspb.Message {
  getNamespaceparam(): string;
  setNamespaceparam(value: string): void;

  getServicename(): string;
  setServicename(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): HealthCheckResultRequest.AsObject;
  static toObject(includeInstance: boolean, msg: HealthCheckResultRequest): HealthCheckResultRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: HealthCheckResultRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): HealthCheckResultRequest;
  static deserializeBinaryFromReader(message: HealthCheckResultRequest, reader: jspb.BinaryReader): HealthCheckResultRequest;
}

export namespace HealthCheckResultRequest {
  export type AsObject = {
    namespaceparam: string,
    servicename: string,
  }
}

export class HealthCheckResultListReply extends jspb.Message {
  clearResultsList(): void;
  getResultsList(): Array<HealthCheckResultReply>;
  setResultsList(value: Array<HealthCheckResultReply>): void;
  addResults(value?: HealthCheckResultReply, index?: number): HealthCheckResultReply;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): HealthCheckResultListReply.AsObject;
  static toObject(includeInstance: boolean, msg: HealthCheckResultListReply): HealthCheckResultListReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: HealthCheckResultListReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): HealthCheckResultListReply;
  static deserializeBinaryFromReader(message: HealthCheckResultListReply, reader: jspb.BinaryReader): HealthCheckResultListReply;
}

export namespace HealthCheckResultListReply {
  export type AsObject = {
    resultsList: Array<HealthCheckResultReply.AsObject>,
  }
}

export class HealthCheckResultReply extends jspb.Message {
  getId(): string;
  setId(value: string): void;

  hasCreationtime(): boolean;
  clearCreationtime(): void;
  getCreationtime(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setCreationtime(value?: google_protobuf_timestamp_pb.Timestamp): void;

  getServiceuid(): string;
  setServiceuid(value: string): void;

  getServicename(): string;
  setServicename(value: string): void;

  getServicenamespace(): string;
  setServicenamespace(value: string): void;

  getStatus(): string;
  setStatus(value: string): void;

  getStringresult(): string;
  setStringresult(value: string): void;

  getCheckedurl(): string;
  setCheckedurl(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): HealthCheckResultReply.AsObject;
  static toObject(includeInstance: boolean, msg: HealthCheckResultReply): HealthCheckResultReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: HealthCheckResultReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): HealthCheckResultReply;
  static deserializeBinaryFromReader(message: HealthCheckResultReply, reader: jspb.BinaryReader): HealthCheckResultReply;
}

export namespace HealthCheckResultReply {
  export type AsObject = {
    id: string,
    creationtime?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    serviceuid: string,
    servicename: string,
    servicenamespace: string,
    status: string,
    stringresult: string,
    checkedurl: string,
  }
}

