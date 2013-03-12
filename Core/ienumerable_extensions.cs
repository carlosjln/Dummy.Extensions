using System.Collections;
using MongoDB.Bson;

namespace DummyExtensions {
	
	public static class ienumerable_extensions {
		public static BsonDocument to_bson_array( this IEnumerable obj, string name ) {
			return obj.to_bson_document().rename_field( "_v" ).to( name );
		}
	}

}