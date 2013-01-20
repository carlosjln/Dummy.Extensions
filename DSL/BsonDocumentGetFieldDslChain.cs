using System;
using MongoDB.Bson;

namespace DummyExtensions {

	public class BsonDocumentGetFieldDslChain {
		private readonly BsonDocument document;
		private readonly string field_name;

		public BsonDocumentGetFieldDslChain(BsonDocument document, string field_name) {
			this.document = document;
			this.field_name = field_name;
		}

		public BsonObjectId as_object_id(BsonObjectId default_value = null) {
			var value = get_value();
			
			return value != null ? value.AsObjectId : default_value;
		}

		public string as_string(string default_value = null) {
			var value = get_value();
			
			return value != null ? value.AsString : default_value;
		}

		// public Boolean as_boolean(bool default_value = false) {
		public Boolean as_boolean(bool default_value = default(bool)) {
			var value = get_value();

			// return value !=null? Convert.ToBoolean(value) : default_value;
			return value !=null? value.AsBoolean : default_value;
		}
		
		public Int32 as_int32(Int32 default_value = 0) {
			var value = get_value();
			
			return value !=null? value.AsInt32 : default_value;
		}

		public BsonArray as_bson_array(BsonArray default_value = null) {
			var value = get_value();

			return value !=null? value.AsBsonArray : default_value;
		}

		public DateTimeOffset as_datetime_offset(DateTimeOffset default_value = default(DateTimeOffset)) {
			var value = get_value();

			return value != null ? value.AsDateTime : default_value;
		}

		public BsonDocument as_bson_document(BsonDocument default_value = null) {
			var value = get_value();

			return value !=null? value.AsBsonDocument : default_value;
		}

		private BsonValue get_value() {
			BsonValue value = null;

			if( document.Contains(field_name) ) {
				value = document.GetValue(field_name);
			}

			return value;
		}


		// public DateTimeOffset as_datetime_offset(DateTimeOffset default_value = default( DateTimeOffset )) {}
	}
}