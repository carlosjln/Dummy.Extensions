using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DummyExtensions {

	public static class string_extensions {

		public static string to_snake_case( this string str ) {
			Func<Match, string> to_snake_case = ( Match match ) => {
				var a = match.Groups[1];
				var b = match.Groups[2];

				return a + "_" + b;
			};
			
			return Regex.Replace( str, @"([a-z])([A-Z]{1})", new MatchEvaluator( to_snake_case ), RegexOptions.None ).ToLower();
		}

		static readonly Dictionary<Type,TypeConverter> type_converters = new Dictionary<Type, TypeConverter>();
		public static object covert_to( this string str, Type type){
			if( str == null ) return null;
			
			if( type_converters.ContainsKey( type ) ) return type_converters[type].ConvertFromInvariantString( str );
			
			var type_converter = TypeDescriptor.GetConverter( type );
			type_converters.Add( type, type_converter );

			return type_converter.ConvertFromInvariantString( str );
		}
	}

}