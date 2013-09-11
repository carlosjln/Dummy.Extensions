using System;

namespace Dummy.Extensions {
	public static class type_extensions {
		public static object get_default_value( this Type type ) {
			return type.IsValueType ? Activator.CreateInstance( type ) : null;
		}

//		public static object get_default_value( this Type type ) {
//			return type.GetMethod( "GetDefaultGeneric" ).MakeGenericMethod( type ).Invoke( type, null );
//		}

	}
}