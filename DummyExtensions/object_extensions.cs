using System;
using System.Collections.Generic;
using System.ComponentModel;

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
		
		// TYPE CONVERTER
		// TODO: Consider using UniversalTypeConverter via Nuget
		static readonly Dictionary<Type,TypeConverter> type_converters = new Dictionary<Type, TypeConverter>();

		public static object convert_to( this object obj, Type type ){
			if( obj == null || obj.ToString().is_null_or_empty() ) {
				return type.get_default_value();
			}

			var type_converter = get_type_converter( type );
			
			if( type_converter != null && type_converter.IsValid(obj) ) {
				return type_converter.ConvertFrom( obj );
			}
			
			throw new Exception( "Can not convert from '{0}' to '{1}'".format(obj.GetType().ToString( ), type.ToString( )) );
		}

		static TypeConverter get_type_converter( Type type){
			if( type_converters.ContainsKey( type ) ) return type_converters[type];
			
			var type_converter = TypeDescriptor.GetConverter( type );
			type_converters.Add( type, type_converter );

			return type_converter;
		}

	}

}