// source: K8sMetrics.proto
/**
 * @fileoverview
 * @enhanceable
 * @suppress {messageConventions} JS Compiler reports an error if a variable or
 *     field starts with 'MSG_' and isn't a translatable message.
 * @public
 */
// GENERATED CODE -- DO NOT EDIT!

var jspb = require('google-protobuf');
var goog = jspb;
var global = Function('return this')();

var google_protobuf_timestamp_pb = require('google-protobuf/google/protobuf/timestamp_pb.js');
goog.object.extend(proto, google_protobuf_timestamp_pb);
var google_protobuf_empty_pb = require('google-protobuf/google/protobuf/empty_pb.js');
goog.object.extend(proto, google_protobuf_empty_pb);
goog.exportSymbol('proto.k8App.NodeMetricListReply', null, global);
goog.exportSymbol('proto.k8App.NodeMetricReply', null, global);
goog.exportSymbol('proto.k8App.UsagePair', null, global);
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.k8App.NodeMetricListReply = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, proto.k8App.NodeMetricListReply.repeatedFields_, null);
};
goog.inherits(proto.k8App.NodeMetricListReply, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.k8App.NodeMetricListReply.displayName = 'proto.k8App.NodeMetricListReply';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.k8App.NodeMetricReply = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, proto.k8App.NodeMetricReply.repeatedFields_, null);
};
goog.inherits(proto.k8App.NodeMetricReply, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.k8App.NodeMetricReply.displayName = 'proto.k8App.NodeMetricReply';
}
/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.k8App.UsagePair = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.k8App.UsagePair, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  /**
   * @public
   * @override
   */
  proto.k8App.UsagePair.displayName = 'proto.k8App.UsagePair';
}

/**
 * List of repeated fields within this message type.
 * @private {!Array<number>}
 * @const
 */
proto.k8App.NodeMetricListReply.repeatedFields_ = [1];



if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.k8App.NodeMetricListReply.prototype.toObject = function(opt_includeInstance) {
  return proto.k8App.NodeMetricListReply.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.k8App.NodeMetricListReply} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.k8App.NodeMetricListReply.toObject = function(includeInstance, msg) {
  var f, obj = {
    metricsList: jspb.Message.toObjectList(msg.getMetricsList(),
    proto.k8App.NodeMetricReply.toObject, includeInstance),
    updatedtime: (f = msg.getUpdatedtime()) && google_protobuf_timestamp_pb.Timestamp.toObject(includeInstance, f)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.k8App.NodeMetricListReply}
 */
proto.k8App.NodeMetricListReply.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.k8App.NodeMetricListReply;
  return proto.k8App.NodeMetricListReply.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.k8App.NodeMetricListReply} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.k8App.NodeMetricListReply}
 */
proto.k8App.NodeMetricListReply.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = new proto.k8App.NodeMetricReply;
      reader.readMessage(value,proto.k8App.NodeMetricReply.deserializeBinaryFromReader);
      msg.addMetrics(value);
      break;
    case 2:
      var value = new google_protobuf_timestamp_pb.Timestamp;
      reader.readMessage(value,google_protobuf_timestamp_pb.Timestamp.deserializeBinaryFromReader);
      msg.setUpdatedtime(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.k8App.NodeMetricListReply.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.k8App.NodeMetricListReply.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.k8App.NodeMetricListReply} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.k8App.NodeMetricListReply.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getMetricsList();
  if (f.length > 0) {
    writer.writeRepeatedMessage(
      1,
      f,
      proto.k8App.NodeMetricReply.serializeBinaryToWriter
    );
  }
  f = message.getUpdatedtime();
  if (f != null) {
    writer.writeMessage(
      2,
      f,
      google_protobuf_timestamp_pb.Timestamp.serializeBinaryToWriter
    );
  }
};


/**
 * repeated NodeMetricReply Metrics = 1;
 * @return {!Array<!proto.k8App.NodeMetricReply>}
 */
proto.k8App.NodeMetricListReply.prototype.getMetricsList = function() {
  return /** @type{!Array<!proto.k8App.NodeMetricReply>} */ (
    jspb.Message.getRepeatedWrapperField(this, proto.k8App.NodeMetricReply, 1));
};


/**
 * @param {!Array<!proto.k8App.NodeMetricReply>} value
 * @return {!proto.k8App.NodeMetricListReply} returns this
*/
proto.k8App.NodeMetricListReply.prototype.setMetricsList = function(value) {
  return jspb.Message.setRepeatedWrapperField(this, 1, value);
};


/**
 * @param {!proto.k8App.NodeMetricReply=} opt_value
 * @param {number=} opt_index
 * @return {!proto.k8App.NodeMetricReply}
 */
proto.k8App.NodeMetricListReply.prototype.addMetrics = function(opt_value, opt_index) {
  return jspb.Message.addToRepeatedWrapperField(this, 1, opt_value, proto.k8App.NodeMetricReply, opt_index);
};


/**
 * Clears the list making it empty but non-null.
 * @return {!proto.k8App.NodeMetricListReply} returns this
 */
proto.k8App.NodeMetricListReply.prototype.clearMetricsList = function() {
  return this.setMetricsList([]);
};


/**
 * optional google.protobuf.Timestamp UpdatedTime = 2;
 * @return {?proto.google.protobuf.Timestamp}
 */
proto.k8App.NodeMetricListReply.prototype.getUpdatedtime = function() {
  return /** @type{?proto.google.protobuf.Timestamp} */ (
    jspb.Message.getWrapperField(this, google_protobuf_timestamp_pb.Timestamp, 2));
};


/**
 * @param {?proto.google.protobuf.Timestamp|undefined} value
 * @return {!proto.k8App.NodeMetricListReply} returns this
*/
proto.k8App.NodeMetricListReply.prototype.setUpdatedtime = function(value) {
  return jspb.Message.setWrapperField(this, 2, value);
};


/**
 * Clears the message field making it undefined.
 * @return {!proto.k8App.NodeMetricListReply} returns this
 */
proto.k8App.NodeMetricListReply.prototype.clearUpdatedtime = function() {
  return this.setUpdatedtime(undefined);
};


/**
 * Returns whether this field is set.
 * @return {boolean}
 */
proto.k8App.NodeMetricListReply.prototype.hasUpdatedtime = function() {
  return jspb.Message.getField(this, 2) != null;
};



/**
 * List of repeated fields within this message type.
 * @private {!Array<number>}
 * @const
 */
proto.k8App.NodeMetricReply.repeatedFields_ = [4];



if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.k8App.NodeMetricReply.prototype.toObject = function(opt_includeInstance) {
  return proto.k8App.NodeMetricReply.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.k8App.NodeMetricReply} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.k8App.NodeMetricReply.toObject = function(includeInstance, msg) {
  var f, obj = {
    name: jspb.Message.getFieldWithDefault(msg, 1, ""),
    timestamp: jspb.Message.getFieldWithDefault(msg, 2, ""),
    window: jspb.Message.getFieldWithDefault(msg, 3, ""),
    usagesList: jspb.Message.toObjectList(msg.getUsagesList(),
    proto.k8App.UsagePair.toObject, includeInstance)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.k8App.NodeMetricReply}
 */
proto.k8App.NodeMetricReply.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.k8App.NodeMetricReply;
  return proto.k8App.NodeMetricReply.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.k8App.NodeMetricReply} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.k8App.NodeMetricReply}
 */
proto.k8App.NodeMetricReply.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {string} */ (reader.readString());
      msg.setName(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setTimestamp(value);
      break;
    case 3:
      var value = /** @type {string} */ (reader.readString());
      msg.setWindow(value);
      break;
    case 4:
      var value = new proto.k8App.UsagePair;
      reader.readMessage(value,proto.k8App.UsagePair.deserializeBinaryFromReader);
      msg.addUsages(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.k8App.NodeMetricReply.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.k8App.NodeMetricReply.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.k8App.NodeMetricReply} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.k8App.NodeMetricReply.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getName();
  if (f.length > 0) {
    writer.writeString(
      1,
      f
    );
  }
  f = message.getTimestamp();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getWindow();
  if (f.length > 0) {
    writer.writeString(
      3,
      f
    );
  }
  f = message.getUsagesList();
  if (f.length > 0) {
    writer.writeRepeatedMessage(
      4,
      f,
      proto.k8App.UsagePair.serializeBinaryToWriter
    );
  }
};


/**
 * optional string Name = 1;
 * @return {string}
 */
proto.k8App.NodeMetricReply.prototype.getName = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 1, ""));
};


/**
 * @param {string} value
 * @return {!proto.k8App.NodeMetricReply} returns this
 */
proto.k8App.NodeMetricReply.prototype.setName = function(value) {
  return jspb.Message.setProto3StringField(this, 1, value);
};


/**
 * optional string Timestamp = 2;
 * @return {string}
 */
proto.k8App.NodeMetricReply.prototype.getTimestamp = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.k8App.NodeMetricReply} returns this
 */
proto.k8App.NodeMetricReply.prototype.setTimestamp = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


/**
 * optional string Window = 3;
 * @return {string}
 */
proto.k8App.NodeMetricReply.prototype.getWindow = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 3, ""));
};


/**
 * @param {string} value
 * @return {!proto.k8App.NodeMetricReply} returns this
 */
proto.k8App.NodeMetricReply.prototype.setWindow = function(value) {
  return jspb.Message.setProto3StringField(this, 3, value);
};


/**
 * repeated UsagePair Usages = 4;
 * @return {!Array<!proto.k8App.UsagePair>}
 */
proto.k8App.NodeMetricReply.prototype.getUsagesList = function() {
  return /** @type{!Array<!proto.k8App.UsagePair>} */ (
    jspb.Message.getRepeatedWrapperField(this, proto.k8App.UsagePair, 4));
};


/**
 * @param {!Array<!proto.k8App.UsagePair>} value
 * @return {!proto.k8App.NodeMetricReply} returns this
*/
proto.k8App.NodeMetricReply.prototype.setUsagesList = function(value) {
  return jspb.Message.setRepeatedWrapperField(this, 4, value);
};


/**
 * @param {!proto.k8App.UsagePair=} opt_value
 * @param {number=} opt_index
 * @return {!proto.k8App.UsagePair}
 */
proto.k8App.NodeMetricReply.prototype.addUsages = function(opt_value, opt_index) {
  return jspb.Message.addToRepeatedWrapperField(this, 4, opt_value, proto.k8App.UsagePair, opt_index);
};


/**
 * Clears the list making it empty but non-null.
 * @return {!proto.k8App.NodeMetricReply} returns this
 */
proto.k8App.NodeMetricReply.prototype.clearUsagesList = function() {
  return this.setUsagesList([]);
};





if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * Optional fields that are not set will be set to undefined.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     net/proto2/compiler/js/internal/generator.cc#kKeyword.
 * @param {boolean=} opt_includeInstance Deprecated. whether to include the
 *     JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @return {!Object}
 */
proto.k8App.UsagePair.prototype.toObject = function(opt_includeInstance) {
  return proto.k8App.UsagePair.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Deprecated. Whether to include
 *     the JSPB instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.k8App.UsagePair} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.k8App.UsagePair.toObject = function(includeInstance, msg) {
  var f, obj = {
    key: jspb.Message.getFieldWithDefault(msg, 1, ""),
    value: jspb.Message.getFieldWithDefault(msg, 2, "")
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.k8App.UsagePair}
 */
proto.k8App.UsagePair.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.k8App.UsagePair;
  return proto.k8App.UsagePair.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.k8App.UsagePair} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.k8App.UsagePair}
 */
proto.k8App.UsagePair.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {string} */ (reader.readString());
      msg.setKey(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setValue(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.k8App.UsagePair.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.k8App.UsagePair.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.k8App.UsagePair} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.k8App.UsagePair.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getKey();
  if (f.length > 0) {
    writer.writeString(
      1,
      f
    );
  }
  f = message.getValue();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
};


/**
 * optional string Key = 1;
 * @return {string}
 */
proto.k8App.UsagePair.prototype.getKey = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 1, ""));
};


/**
 * @param {string} value
 * @return {!proto.k8App.UsagePair} returns this
 */
proto.k8App.UsagePair.prototype.setKey = function(value) {
  return jspb.Message.setProto3StringField(this, 1, value);
};


/**
 * optional string Value = 2;
 * @return {string}
 */
proto.k8App.UsagePair.prototype.getValue = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/**
 * @param {string} value
 * @return {!proto.k8App.UsagePair} returns this
 */
proto.k8App.UsagePair.prototype.setValue = function(value) {
  return jspb.Message.setProto3StringField(this, 2, value);
};


goog.object.extend(exports, proto.k8App);
