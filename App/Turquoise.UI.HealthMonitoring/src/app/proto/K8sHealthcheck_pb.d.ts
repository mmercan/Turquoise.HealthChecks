// package: k8App
// file: K8sHealthcheck.proto

import * as jspb from "google-protobuf";
import * as google_protobuf_timestamp_pb from "google-protobuf/google/protobuf/timestamp_pb";
import * as google_protobuf_empty_pb from "google-protobuf/google/protobuf/empty_pb";

export class NamespaceListReply extends jspb.Message {
  clearNamespacesList(): void;
  getNamespacesList(): Array<string>;
  setNamespacesList(value: Array<string>): void;
  addNamespaces(value: string, index?: number): string;

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
    namespacesList: Array<string>,
  }
}

export class DeploymentReply extends jspb.Message {
  hasDatetimestamp(): boolean;
  clearDatetimestamp(): void;
  getDatetimestamp(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setDatetimestamp(value?: google_protobuf_timestamp_pb.Timestamp): void;

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
    datetimestamp?: google_protobuf_timestamp_pb.Timestamp.AsObject,
  }
}

