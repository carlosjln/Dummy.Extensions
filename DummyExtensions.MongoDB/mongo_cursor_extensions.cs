using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DummyExtensions {

	public static class mongo_cursor_extensions {

		public static List<BsonDocument> as_list( this MongoCursor cursor ) {
			var result = new List<BsonDocument>();
			
			foreach( BsonDocument document in cursor ) {
				result.Add( document );
			}
			
			cursor.Server.Disconnect();

			return result;
		}

		public static BsonArray as_bson_array( this MongoCursor cursor ) {
			var result = new BsonArray();
			
			foreach( BsonDocument document in cursor ) {
				result.Add( document );
			}
			
			cursor.Server.Disconnect();
			
			return result;
		}

		public static List<T> as_list_of<T>( this MongoCursor<T> cursor ) {
			var result = new List<T>();
			
			foreach( var document in cursor ) {
				result.Add( document );
			}
			
			cursor.Server.Disconnect();

			return result;
		}


	}

}