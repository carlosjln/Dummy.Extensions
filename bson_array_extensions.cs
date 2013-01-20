using MongoDB.Bson;

namespace DummyExtensions {

	public static class bson_array_extensions {

		public static BsonArrayGetFieldExtensionChain get_field(this BsonArray array, int field) {
			return new BsonArrayGetFieldExtensionChain( array, field );
		}

	}

}