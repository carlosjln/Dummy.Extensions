using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using JsonWriter = DummyExtensions.Externals.JsonWriter;

namespace DummyExtensions {

	public static class object_extensions {
		static readonly Dictionary<object, PropertyInfo[]> object_properties = new Dictionary<object, PropertyInfo []>();
		
		public static BsonDocument to_bson_document( this object self, BsonSettings bson_settings ) {
			var document = self.ToBsonDocument();
			
			if( bson_settings.IgnoreDiscriminator ) document.Remove( "_t" );

			if( bson_settings.NormalizeId && document.Contains( "_id" ) ) {
				string id = null;
				var document_id = document["_id"];
				document.Remove( "_id" );

				// TODO: this is critical
				if( document_id != null ) {
					if( document_id.IsBsonBinaryData ) {
						id = document_id.AsBsonBinaryData.RawValue.ToString( );
					} else if( document_id.IsString ) {
						id = document_id.AsString;
					}
				}

				if( id != null ) document.InsertAt( 0, new BsonElement( "id", id ) );
			}

			return document;
		}
		
		public static BsonDocument to_bson_document( this object self) {
			return to_bson_document(self, BsonSettings.Defaults );
		}

		public static string to_json( this object self ) {
			return to_json(self, JsonWriterSettings.Defaults, BsonSettings.Defaults );
		}

		public static string to_json( this object self, JsonWriterSettings json_writer_settings, BsonSettings bson_settings ) {
			using ( var string_writer = new StringWriter() ){
				using ( var bson_writer = new JsonWriter(string_writer, json_writer_settings, bson_settings) ) {
					BsonSerializer.Serialize(bson_writer, self);
				}
				
				return string_writer.ToString();
			}
		}
	}

}