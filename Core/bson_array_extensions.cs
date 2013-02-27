using DummyExtensions.DSL;
using MongoDB.Bson;

namespace DummyExtensions {

	public static class bson_array_extensions {

		public static BsonArrayGetFieldExtensionDslChain get_field(this BsonArray array, int field) {
			return new BsonArrayGetFieldExtensionDslChain( array, field );
		}

	}

}