// package: k8App
// file: K8sHealthcheck.proto

import * as jspb from "google-protobuf";
import * as google_protobuf_timestamp_pb from "google-protobuf/google/protobuf/timestamp_pb";
import * as google_protobuf_empty_pb from "google-protobuf/google/protobuf/empty_pb";
import * as google_protobuf_wrappers_pb from "google-protobuf/google/protobuf/wrappers_pb";

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

export class GetDeploymentRequest extends jspb.Message {
  getNamespace(): string;
  setNamespace(value: string): void;

  getDeploymentname(): string;
  setDeploymentname(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetDeploymentRequest.AsObject;
  static toObject(includeInstance: boolean, msg: GetDeploymentRequest): GetDeploymentRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: GetDeploymentRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetDeploymentRequest;
  static deserializeBinaryFromReader(message: GetDeploymentRequest, reader: jspb.BinaryReader): GetDeploymentRequest;
}

export namespace GetDeploymentRequest {
  export type AsObject = {
    namespace: string,
    deploymentname: string,
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

  getNamespace(): string;
  setNamespace(value: string): void;

  clearAnnotationsList(): void;
  getAnnotationsList(): Array<Pair>;
  setAnnotationsList(value: Array<Pair>): void;
  addAnnotations(value?: Pair, index?: number): Pair;

  clearLabelsList(): void;
  getLabelsList(): Array<Pair>;
  setLabelsList(value: Array<Pair>): void;
  addLabels(value?: Pair, index?: number): Pair;

  hasSpec(): boolean;
  clearSpec(): void;
  getSpec(): DeploymentSpecReply | undefined;
  setSpec(value?: DeploymentSpecReply): void;

  hasStatus(): boolean;
  clearStatus(): void;
  getStatus(): DeploymentStatusReply | undefined;
  setStatus(value?: DeploymentStatusReply): void;

  hasCreationtime(): boolean;
  clearCreationtime(): void;
  getCreationtime(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setCreationtime(value?: google_protobuf_timestamp_pb.Timestamp): void;

  getCrondescriptionscaleup(): string;
  setCrondescriptionscaleup(value: string): void;

  getCrondescriptionscaledown(): string;
  setCrondescriptionscaledown(value: string): void;

  getDownscalecrontab(): string;
  setDownscalecrontab(value: string): void;

  getUpscalecrontab(): string;
  setUpscalecrontab(value: string): void;

  getDownscalereplica(): string;
  setDownscalereplica(value: string): void;

  getUpscalereplica(): string;
  setUpscalereplica(value: string): void;

  getCrontabtimezone(): string;
  setCrontabtimezone(value: string): void;

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
    namespace: string,
    annotationsList: Array<Pair.AsObject>,
    labelsList: Array<Pair.AsObject>,
    spec?: DeploymentSpecReply.AsObject,
    status?: DeploymentStatusReply.AsObject,
    creationtime?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    crondescriptionscaleup: string,
    crondescriptionscaledown: string,
    downscalecrontab: string,
    upscalecrontab: string,
    downscalereplica: string,
    upscalereplica: string,
    crontabtimezone: string,
  }
}

export class DeploymentSpecReply extends jspb.Message {
  hasProgressdeadlineseconds(): boolean;
  clearProgressdeadlineseconds(): void;
  getProgressdeadlineseconds(): google_protobuf_wrappers_pb.Int32Value | undefined;
  setProgressdeadlineseconds(value?: google_protobuf_wrappers_pb.Int32Value): void;

  hasReplicas(): boolean;
  clearReplicas(): void;
  getReplicas(): google_protobuf_wrappers_pb.Int32Value | undefined;
  setReplicas(value?: google_protobuf_wrappers_pb.Int32Value): void;

  hasRevisionhistorylimit(): boolean;
  clearRevisionhistorylimit(): void;
  getRevisionhistorylimit(): google_protobuf_wrappers_pb.Int32Value | undefined;
  setRevisionhistorylimit(value?: google_protobuf_wrappers_pb.Int32Value): void;

  getSelectorstring(): string;
  setSelectorstring(value: string): void;

  getImage(): string;
  setImage(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): DeploymentSpecReply.AsObject;
  static toObject(includeInstance: boolean, msg: DeploymentSpecReply): DeploymentSpecReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: DeploymentSpecReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): DeploymentSpecReply;
  static deserializeBinaryFromReader(message: DeploymentSpecReply, reader: jspb.BinaryReader): DeploymentSpecReply;
}

export namespace DeploymentSpecReply {
  export type AsObject = {
    progressdeadlineseconds?: google_protobuf_wrappers_pb.Int32Value.AsObject,
    replicas?: google_protobuf_wrappers_pb.Int32Value.AsObject,
    revisionhistorylimit?: google_protobuf_wrappers_pb.Int32Value.AsObject,
    selectorstring: string,
    image: string,
  }
}

export class DeploymentStatusReply extends jspb.Message {
  hasAvailablereplicas(): boolean;
  clearAvailablereplicas(): void;
  getAvailablereplicas(): google_protobuf_wrappers_pb.Int32Value | undefined;
  setAvailablereplicas(value?: google_protobuf_wrappers_pb.Int32Value): void;

  hasCollisioncount(): boolean;
  clearCollisioncount(): void;
  getCollisioncount(): google_protobuf_wrappers_pb.Int32Value | undefined;
  setCollisioncount(value?: google_protobuf_wrappers_pb.Int32Value): void;

  hasReadyreplicas(): boolean;
  clearReadyreplicas(): void;
  getReadyreplicas(): google_protobuf_wrappers_pb.Int32Value | undefined;
  setReadyreplicas(value?: google_protobuf_wrappers_pb.Int32Value): void;

  hasReplicas(): boolean;
  clearReplicas(): void;
  getReplicas(): google_protobuf_wrappers_pb.Int32Value | undefined;
  setReplicas(value?: google_protobuf_wrappers_pb.Int32Value): void;

  hasUnavailablereplicas(): boolean;
  clearUnavailablereplicas(): void;
  getUnavailablereplicas(): google_protobuf_wrappers_pb.Int32Value | undefined;
  setUnavailablereplicas(value?: google_protobuf_wrappers_pb.Int32Value): void;

  hasUpdatedreplicas(): boolean;
  clearUpdatedreplicas(): void;
  getUpdatedreplicas(): google_protobuf_wrappers_pb.Int32Value | undefined;
  setUpdatedreplicas(value?: google_protobuf_wrappers_pb.Int32Value): void;

  clearConditionList(): void;
  getConditionList(): Array<DeploymentStatusConditionReply>;
  setConditionList(value: Array<DeploymentStatusConditionReply>): void;
  addCondition(value?: DeploymentStatusConditionReply, index?: number): DeploymentStatusConditionReply;

  getOverallstatus(): string;
  setOverallstatus(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): DeploymentStatusReply.AsObject;
  static toObject(includeInstance: boolean, msg: DeploymentStatusReply): DeploymentStatusReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: DeploymentStatusReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): DeploymentStatusReply;
  static deserializeBinaryFromReader(message: DeploymentStatusReply, reader: jspb.BinaryReader): DeploymentStatusReply;
}

export namespace DeploymentStatusReply {
  export type AsObject = {
    availablereplicas?: google_protobuf_wrappers_pb.Int32Value.AsObject,
    collisioncount?: google_protobuf_wrappers_pb.Int32Value.AsObject,
    readyreplicas?: google_protobuf_wrappers_pb.Int32Value.AsObject,
    replicas?: google_protobuf_wrappers_pb.Int32Value.AsObject,
    unavailablereplicas?: google_protobuf_wrappers_pb.Int32Value.AsObject,
    updatedreplicas?: google_protobuf_wrappers_pb.Int32Value.AsObject,
    conditionList: Array<DeploymentStatusConditionReply.AsObject>,
    overallstatus: string,
  }
}

export class DeploymentStatusConditionReply extends jspb.Message {
  hasLasttransitiontime(): boolean;
  clearLasttransitiontime(): void;
  getLasttransitiontime(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setLasttransitiontime(value?: google_protobuf_timestamp_pb.Timestamp): void;

  hasLastupdatetime(): boolean;
  clearLastupdatetime(): void;
  getLastupdatetime(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setLastupdatetime(value?: google_protobuf_timestamp_pb.Timestamp): void;

  getMessage(): string;
  setMessage(value: string): void;

  getReason(): string;
  setReason(value: string): void;

  getStatus(): string;
  setStatus(value: string): void;

  getType(): string;
  setType(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): DeploymentStatusConditionReply.AsObject;
  static toObject(includeInstance: boolean, msg: DeploymentStatusConditionReply): DeploymentStatusConditionReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: DeploymentStatusConditionReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): DeploymentStatusConditionReply;
  static deserializeBinaryFromReader(message: DeploymentStatusConditionReply, reader: jspb.BinaryReader): DeploymentStatusConditionReply;
}

export namespace DeploymentStatusConditionReply {
  export type AsObject = {
    lasttransitiontime?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    lastupdatetime?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    message: string,
    reason: string,
    status: string,
    type: string,
  }
}

export class DeploymentScaleHistoryListReply extends jspb.Message {
  clearHistoriesList(): void;
  getHistoriesList(): Array<DeploymentScaleHistoryReply>;
  setHistoriesList(value: Array<DeploymentScaleHistoryReply>): void;
  addHistories(value?: DeploymentScaleHistoryReply, index?: number): DeploymentScaleHistoryReply;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): DeploymentScaleHistoryListReply.AsObject;
  static toObject(includeInstance: boolean, msg: DeploymentScaleHistoryListReply): DeploymentScaleHistoryListReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: DeploymentScaleHistoryListReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): DeploymentScaleHistoryListReply;
  static deserializeBinaryFromReader(message: DeploymentScaleHistoryListReply, reader: jspb.BinaryReader): DeploymentScaleHistoryListReply;
}

export namespace DeploymentScaleHistoryListReply {
  export type AsObject = {
    historiesList: Array<DeploymentScaleHistoryReply.AsObject>,
  }
}

export class DeploymentScaleHistoryReply extends jspb.Message {
  getUid(): string;
  setUid(value: string): void;

  getName(): string;
  setName(value: string): void;

  getNamespace(): string;
  setNamespace(value: string): void;

  getSchedule(): string;
  setSchedule(value: string): void;

  getTimezone(): string;
  setTimezone(value: string): void;

  hasScaledutc(): boolean;
  clearScaledutc(): void;
  getScaledutc(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setScaledutc(value?: google_protobuf_timestamp_pb.Timestamp): void;

  getOldscalenumber(): number;
  setOldscalenumber(value: number): void;

  getNewscalenumber(): number;
  setNewscalenumber(value: number): void;

  getStatus(): string;
  setStatus(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): DeploymentScaleHistoryReply.AsObject;
  static toObject(includeInstance: boolean, msg: DeploymentScaleHistoryReply): DeploymentScaleHistoryReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: DeploymentScaleHistoryReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): DeploymentScaleHistoryReply;
  static deserializeBinaryFromReader(message: DeploymentScaleHistoryReply, reader: jspb.BinaryReader): DeploymentScaleHistoryReply;
}

export namespace DeploymentScaleHistoryReply {
  export type AsObject = {
    uid: string,
    name: string,
    namespace: string,
    schedule: string,
    timezone: string,
    scaledutc?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    oldscalenumber: number,
    newscalenumber: number,
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

export class Mertic extends jspb.Message {
  getKey(): string;
  setKey(value: string): void;

  getValue(): string;
  setValue(value: string): void;

  getFormat(): string;
  setFormat(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): Mertic.AsObject;
  static toObject(includeInstance: boolean, msg: Mertic): Mertic.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: Mertic, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): Mertic;
  static deserializeBinaryFromReader(message: Mertic, reader: jspb.BinaryReader): Mertic;
}

export namespace Mertic {
  export type AsObject = {
    key: string,
    value: string,
    format: string,
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

export class NodeListReply extends jspb.Message {
  clearNodesList(): void;
  getNodesList(): Array<NodeReply>;
  setNodesList(value: Array<NodeReply>): void;
  addNodes(value?: NodeReply, index?: number): NodeReply;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): NodeListReply.AsObject;
  static toObject(includeInstance: boolean, msg: NodeListReply): NodeListReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: NodeListReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): NodeListReply;
  static deserializeBinaryFromReader(message: NodeListReply, reader: jspb.BinaryReader): NodeListReply;
}

export namespace NodeListReply {
  export type AsObject = {
    nodesList: Array<NodeReply.AsObject>,
  }
}

export class NodeReply extends jspb.Message {
  getName(): string;
  setName(value: string): void;

  getUid(): string;
  setUid(value: string): void;

  getProviderid(): string;
  setProviderid(value: string): void;

  clearLabelsList(): void;
  getLabelsList(): Array<Pair>;
  setLabelsList(value: Array<Pair>): void;
  addLabels(value?: Pair, index?: number): Pair;

  clearAnnotationsList(): void;
  getAnnotationsList(): Array<Pair>;
  setAnnotationsList(value: Array<Pair>): void;
  addAnnotations(value?: Pair, index?: number): Pair;

  getStatus(): string;
  setStatus(value: string): void;

  clearConditionsList(): void;
  getConditionsList(): Array<NodeReplyCondition>;
  setConditionsList(value: Array<NodeReplyCondition>): void;
  addConditions(value?: NodeReplyCondition, index?: number): NodeReplyCondition;

  clearAllocatablesList(): void;
  getAllocatablesList(): Array<Mertic>;
  setAllocatablesList(value: Array<Mertic>): void;
  addAllocatables(value?: Mertic, index?: number): Mertic;

  clearCapacitiesList(): void;
  getCapacitiesList(): Array<Mertic>;
  setCapacitiesList(value: Array<Mertic>): void;
  addCapacities(value?: Mertic, index?: number): Mertic;

  clearUsageList(): void;
  getUsageList(): Array<Mertic>;
  setUsageList(value: Array<Mertic>): void;
  addUsage(value?: Mertic, index?: number): Mertic;

  clearImagesList(): void;
  getImagesList(): Array<NodeReplyImage>;
  setImagesList(value: Array<NodeReplyImage>): void;
  addImages(value?: NodeReplyImage, index?: number): NodeReplyImage;

  hasNodeinfo(): boolean;
  clearNodeinfo(): void;
  getNodeinfo(): NodeInfo | undefined;
  setNodeinfo(value?: NodeInfo): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): NodeReply.AsObject;
  static toObject(includeInstance: boolean, msg: NodeReply): NodeReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: NodeReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): NodeReply;
  static deserializeBinaryFromReader(message: NodeReply, reader: jspb.BinaryReader): NodeReply;
}

export namespace NodeReply {
  export type AsObject = {
    name: string,
    uid: string,
    providerid: string,
    labelsList: Array<Pair.AsObject>,
    annotationsList: Array<Pair.AsObject>,
    status: string,
    conditionsList: Array<NodeReplyCondition.AsObject>,
    allocatablesList: Array<Mertic.AsObject>,
    capacitiesList: Array<Mertic.AsObject>,
    usageList: Array<Mertic.AsObject>,
    imagesList: Array<NodeReplyImage.AsObject>,
    nodeinfo?: NodeInfo.AsObject,
  }
}

export class NodeReplyCondition extends jspb.Message {
  hasLastheartbeattime(): boolean;
  clearLastheartbeattime(): void;
  getLastheartbeattime(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setLastheartbeattime(value?: google_protobuf_timestamp_pb.Timestamp): void;

  hasLasttransitiontime(): boolean;
  clearLasttransitiontime(): void;
  getLasttransitiontime(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setLasttransitiontime(value?: google_protobuf_timestamp_pb.Timestamp): void;

  getMessage(): string;
  setMessage(value: string): void;

  getReason(): string;
  setReason(value: string): void;

  getStatus(): string;
  setStatus(value: string): void;

  getType(): string;
  setType(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): NodeReplyCondition.AsObject;
  static toObject(includeInstance: boolean, msg: NodeReplyCondition): NodeReplyCondition.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: NodeReplyCondition, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): NodeReplyCondition;
  static deserializeBinaryFromReader(message: NodeReplyCondition, reader: jspb.BinaryReader): NodeReplyCondition;
}

export namespace NodeReplyCondition {
  export type AsObject = {
    lastheartbeattime?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    lasttransitiontime?: google_protobuf_timestamp_pb.Timestamp.AsObject,
    message: string,
    reason: string,
    status: string,
    type: string,
  }
}

export class NodeReplyImage extends jspb.Message {
  getName(): string;
  setName(value: string): void;

  getSize(): string;
  setSize(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): NodeReplyImage.AsObject;
  static toObject(includeInstance: boolean, msg: NodeReplyImage): NodeReplyImage.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: NodeReplyImage, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): NodeReplyImage;
  static deserializeBinaryFromReader(message: NodeReplyImage, reader: jspb.BinaryReader): NodeReplyImage;
}

export namespace NodeReplyImage {
  export type AsObject = {
    name: string,
    size: string,
  }
}

export class NodeInfo extends jspb.Message {
  getArchitecture(): string;
  setArchitecture(value: string): void;

  getBootid(): string;
  setBootid(value: string): void;

  getContainerruntimeversion(): string;
  setContainerruntimeversion(value: string): void;

  getKernelversion(): string;
  setKernelversion(value: string): void;

  getKubeproxyversion(): string;
  setKubeproxyversion(value: string): void;

  getKubeletversion(): string;
  setKubeletversion(value: string): void;

  getMachineid(): string;
  setMachineid(value: string): void;

  getOperatingsystem(): string;
  setOperatingsystem(value: string): void;

  getOsimage(): string;
  setOsimage(value: string): void;

  getSystemuuid(): string;
  setSystemuuid(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): NodeInfo.AsObject;
  static toObject(includeInstance: boolean, msg: NodeInfo): NodeInfo.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: NodeInfo, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): NodeInfo;
  static deserializeBinaryFromReader(message: NodeInfo, reader: jspb.BinaryReader): NodeInfo;
}

export namespace NodeInfo {
  export type AsObject = {
    architecture: string,
    bootid: string,
    containerruntimeversion: string,
    kernelversion: string,
    kubeproxyversion: string,
    kubeletversion: string,
    machineid: string,
    operatingsystem: string,
    osimage: string,
    systemuuid: string,
  }
}

