using MongoDB.Bson;
using MongoDB.Driver;

namespace DummyExtensions {

	public static class bson_document_extensions {
		// FIELD GETTERS
		public static ObjectId? get_id( this BsonDocument document ) {
			ObjectId? value = null;
			const string id = "_id";

			if( document.Contains( id ) ) {
				value = document.GetValue( id ).AsObjectId;
			}
			
			return value;
		}

		public static BsonDocumentGetFieldDslChain get_field( this BsonDocument document, string field_name ) {
			return new BsonDocumentGetFieldDslChain( document, field_name );
		}

		// WRAPPERS
		public static UpdateDocument as_update_document( this BsonDocument document ) {
			return new UpdateDocument( "$set", document );
		}

		// MODIFIERS
		public static BsonDocumentRenameFieldDslChain rename_field( this BsonDocument document, string field_name ) {
			return new BsonDocumentRenameFieldDslChain( document, field_name );
		}
	}

}