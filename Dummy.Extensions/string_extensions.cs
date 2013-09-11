using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Dummy.Extensions {

	public static class string_extensions {
		// BOOLEANS
		public static bool is_null( this string s ) {
			return s == null;
		}

		public static bool is_null_or_empty( this string s ) {
			return string.IsNullOrEmpty( s );
		}

		public static bool is_not_null( this string s ) {
			return  s != null;
		}

		public static bool is_not_null_or_empty( this string s ) {
			return !string.IsNullOrEmpty( s );
		}

		public static bool is_empty( this string s ) {
			return s == string.Empty;
		}
		public static bool is_not_empty( this string s ) {
			return string.IsNullOrEmpty( s ) == false;
		}

		public static bool matches( this string str, string regex, RegexOptions options = RegexOptions.None ) {
			return Regex.IsMatch( str, regex, options );
		}
		
		// E-mail validator
		public static bool is_valid_email( this string str ) {
			if( String.IsNullOrEmpty( str ) ) return false;

			str = str.Trim();

			if( str.Length > 254 ) return false;

			// Use IdnMapping class to convert Unicode domain names.
			try {
				str = Regex.Replace( str, @"(@)(.+)$", DomainMapper, RegexOptions.None );
			} catch( Exception ) {
				return false;
			}
			
			// Return true if strIn is in valid e-mail format. 
			try {
				const string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
				return Regex.IsMatch( str, pattern, RegexOptions.IgnoreCase );
			} catch( Exception ) {
				return false;
			}
		}

		static string DomainMapper( Match match ) {
			// IdnMapping class with default property values.
			var idn = new IdnMapping( );
			var domain_name = match.Groups[ 2 ].Value;

			domain_name = idn.GetAscii( domain_name );

			return match.Groups[ 1 ].Value + domain_name;
		}


		// MODIFIERS
		public static void print( this string str ) {
			HttpContext.Current.Response.Write( str );
		}

		public static string remove_html_comments( this string str ) {
			return str.Replace( "<!--(.*?)-->", "" );
		}

		public static string remove_tabs( this string str ) {
			return str.Replace( "\t", "" );
		}

		public static string remove_new_line_delimeter( this string str ) {
			return str.Replace( "\r\n", "" );
		}

		public static string escape_backslashes( this string str ) {
			return str.Replace( @"\", @"\\" );
		}

		public static string escape_single_quotes( this string str ) {
			return str.Replace( "'", @"\'" );
		}

		public static string escape_double_quotes( this string str ) {
			return str.Replace( "\"", @"\\" + @"""" );
		}


		// BASE64 ENCODING
		public static string encode_to_base64( this string str ) {
			return str.is_null_or_empty() ? null : Convert.ToBase64String( Encoding.UTF8.GetBytes( str ) );
		}

		public static string decode_from_base64( this string str ) {
			return str.is_null_or_empty() ? null : Encoding.UTF8.GetString( System.Convert.FromBase64String( str ) );
		}

		
		// CRYPTOGRAPHY
		public static string to_string( this byte[ ] bytes ) {
			var output = new StringBuilder( "" );
			
			for( var i = 0; i < bytes.Length; i++ ) {
				output.Append( bytes[i].ToString( "x2" ) );
			}

			return output.ToString();
		}

		public static string encrypt_using_sha256( this string str ) {
			var sha = new SHA256Managed();

			var input_bytes = Encoding.UTF8.GetBytes( str );
			var hashed_bytes = sha.ComputeHash( input_bytes );

			return hashed_bytes.to_string();
		}

		public static string encrypt_using_sha384( this string str ) {
			var sha = new SHA384Managed();

			var input_bytes = Encoding.UTF8.GetBytes( str );
			var hashed_bytes = sha.ComputeHash( input_bytes );

			return hashed_bytes.to_string();
		}

		public static string encrypt_using_sha512( this string str ) {
			var sha = new SHA512Managed();

			var input_bytes = Encoding.UTF8.GetBytes( str );
			var hashed_bytes = sha.ComputeHash( input_bytes );

			return hashed_bytes.to_string();
		}


		// FORMAT
		public static string format( this string str, params string[] values ) {
			return String.IsNullOrEmpty(str) ? "" : string.Format( str, values );
		}

		public static string to_snake_case( this string str ) {
			Func<Match, string> to_snake_case = ( Match match ) => {
				var a = match.Groups[1];
				var b = match.Groups[2];
				
				return a + "_" + b;
			};
			
			return Regex.Replace( str, @"([a-z])([A-Z]{1})", new MatchEvaluator( to_snake_case ), RegexOptions.None ).ToLower();
		}
		
		public static string wordify( this string str, string spacer = " " ) {
			if( !Regex.IsMatch( str, "[a-z]" ) ) return str;

			return string.Join( spacer, Regex.Split( str, @"(?<!^)(?=[A-Z])" ) );
		}
	}

}