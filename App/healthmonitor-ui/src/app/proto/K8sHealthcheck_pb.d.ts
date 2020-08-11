// package: k8App
// file: K8sHealthcheck.proto

import * as jspb from "google-protobuf";
import * as google_protobuf_timestamp_pb from "google-protobuf/google/protobuf/timestamp_pb";
import * as google_protobuf_empty_pb from "google-protobuf/google/protobuf/empty_pb";

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

