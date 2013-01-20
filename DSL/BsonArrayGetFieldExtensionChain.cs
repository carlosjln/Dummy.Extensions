using System;
using MongoDB.Bson;

namespace DummyExtensions {

	public class BsonArrayGetFieldExtensionChain {
		private readonly BsonArray array;
		private readonly int field;

		public BsonArrayGetFieldExtensionChain(BsonArray array, int field) {
			this.array = array;
			this.field = field;
		}

		public string as_string(string default_value = null) {
			try {
				return array[field].AsString;
			}catch(Exception) {
				return default_value;
			}
		}

		public Boolean as_boolean(bool default_value = false) {
			try {
				return array[field].AsBoolean;
			}catch(Exception) {
				return default_value;
			}
		}
		
		public Int32 as_int32(Int32 default_value = 0) {
			try {
				return array[field].AsInt32;
			}catch(Exception) {
				return default_value;
			}
		}
	}

}