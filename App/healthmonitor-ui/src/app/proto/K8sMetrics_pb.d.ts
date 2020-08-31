// package: k8App
// file: K8sMetrics.proto

import * as jspb from "google-protobuf";
import * as google_protobuf_timestamp_pb from "google-protobuf/google/protobuf/timestamp_pb";
import * as google_protobuf_empty_pb from "google-protobuf/google/protobuf/empty_pb";

export class NodeMetricListReply extends jspb.Message {
  clearMetricsList(): void;
  getMetricsList(): Array<NodeMetricReply>;
  setMetricsList(value: Array<NodeMetricReply>): void;
  addMetrics(value?: NodeMetricReply, index?: number): NodeMetricReply;

  hasUpdatedtime(): boolean;
  clearUpdatedtime(): void;
  getUpdatedtime(): google_protobuf_timestamp_pb.Timestamp | undefined;
  setUpdatedtime(value?: google_protobuf_timestamp_pb.Timestamp): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): NodeMetricListReply.AsObject;
  static toObject(includeInstance: boolean, msg: NodeMetricListReply): NodeMetricListReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: NodeMetricListReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): NodeMetricListReply;
  static deserializeBinaryFromReader(message: NodeMetricListReply, reader: jspb.BinaryReader): NodeMetricListReply;
}

export namespace NodeMetricListReply {
  export type AsObject = {
    metricsList: Array<NodeMetricReply.AsObject>,
    updatedtime?: google_protobuf_timestamp_pb.Timestamp.AsObject,
  }
}

export class NodeMetricReply extends jspb.Message {
  getName(): string;
  setName(value: string): void;

  getTimestamp(): string;
  setTimestamp(value: string): void;

  getWindow(): string;
  setWindow(value: string): void;

  clearUsagesList(): void;
  getUsagesList(): Array<UsagePair>;
  setUsagesList(value: Array<UsagePair>): void;
  addUsages(value?: UsagePair, index?: number): UsagePair;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): NodeMetricReply.AsObject;
  static toObject(includeInstance: boolean, msg: NodeMetricReply): NodeMetricReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: NodeMetricReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): NodeMetricReply;
  static deserializeBinaryFromReader(message: NodeMetricReply, reader: jspb.BinaryReader): NodeMetricReply;
}

export namespace NodeMetricReply {
  export type AsObject = {
    name: string,
    timestamp: string,
    window: string,
    usagesList: Array<UsagePair.AsObject>,
  }
}

export class UsagePair extends jspb.Message {
  getKey(): string;
  setKey(value: string): void;

  getValue(): string;
  setValue(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): UsagePair.AsObject;
  static toObject(includeInstance: boolean, msg: UsagePair): UsagePair.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: UsagePair, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): UsagePair;
  static deserializeBinaryFromReader(message: UsagePair, reader: jspb.BinaryReader): UsagePair;
}

export namespace UsagePair {
  export type AsObject = {
    key: string,
    value: string,
  }
}

