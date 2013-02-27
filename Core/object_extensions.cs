using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using DummyExtensions.Externals;
using DummyObjects;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using JsonWriter = DummyExtensions.Externals.JsonWriter;

namespace DummyExtensions {

	public static class object_extensions {

		// INHERITANCE
		static readonly Type type_of_object = typeof( object );

		public static bool inherits_from( this object reference, Type type ) {
			if( reference == null ) return false;

			return inherits_from( reference.GetType(), type );
		}

		public static bool inherits_from( this Type reference_type, Type type ) {
			if( reference_type == null || reference_type.BaseType == null) return false;
			
			var base_type = reference_type.BaseType;
			
			while( base_type != type_of_object ) {
				if( base_type == type  || (base_type.IsGenericType && base_type.GetGenericTypeDefinition() == type) ) {
					return true;
				}

				base_type = base_type.BaseType;
			}

			return false;
		}

		public static bool implements( this object self, Type interface_type ) {
			return implements( self.GetType(), interface_type );
		}
		public static bool implements( this Type self, Type interface_type ) {
			return self.GetInterface( interface_type.Name ) != null;
		}

		public static Type get_interface( this object self, Type interface_type ) {
			return get_interface( self.GetType(), interface_type );
		}
		public static Type get_interface( this Type self, Type interface_type ) {
			return self.GetInterface( interface_type.Name );
		}


		// BSON
		public static BsonDocument to_bson_document( this object self, BsonSettings bson_settings ) {
			var document = self.ToBsonDocument();
			
			if( bson_settings.IgnoreDiscriminator ) document.Remove( "_t" );

			if( bson_settings.NormalizeId && document.Contains( "_id" ) ) {
				string id = null;
				var document_id = document["_id"];
				document.Remove( "_id" );

				// This is critical
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

		
		// JSON
		public static string to_json( this object obj ) {
			return to_json(obj, JsonWriterSettings.Defaults, BsonSettings.Defaults );
		}

		public static string to_json( this object obj, JsonWriterSettings json_writer_settings, BsonSettings bson_settings ) {
			using ( var string_writer = new StringWriter() ){
				using ( var bson_writer = new JsonWriter(string_writer, json_writer_settings, bson_settings) ) {
					BsonSerializer.Serialize(bson_writer, obj);
				}
				
				return string_writer.ToString();
			}
		}


		// HTML
		public static HtmlBuilder to_html( this object obj ) {
			return new HtmlBuilder( obj );
		}


		// TYPE CONVERTER
		static readonly Dictionary<Type,TypeConverter> type_converters = new Dictionary<Type, TypeConverter>();

		public static object covert_to( this string str, Type type){
			var type_converter = get_type_converter( type );
			
			if( type_converter.IsValid( str ) ) return type_converter.ConvertFromInvariantString( str );
			
			if( Nullable.GetUnderlyingType( type ) != null ) return null;

			return  default( Type );
		}

		static TypeConverter get_type_converter( Type type){
			if( type_converters.ContainsKey( type ) ) return type_converters[type];
			
			var type_converter = TypeDescriptor.GetConverter( type );
			type_converters.Add( type, type_converter );

			return type_converter;
		}

	}

}