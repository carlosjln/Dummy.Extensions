using MongoDB.Bson;

namespace DummyExtensions {

	public class BsonDocumentRenameFieldDslChain {
		private readonly string field_name;
		private readonly BsonDocument document;

		public BsonDocumentRenameFieldDslChain(BsonDocument document, string field_name) {
			this.document = document;
			this.field_name = field_name;
		}

		public BsonDocument to(string new_field_name) {
			if( document.Contains(field_name) ) {
				document[new_field_name] = document[field_name];
				document.Remove(field_name);
			}else {
				document[new_field_name] = null;
			}

			return document;
		}
	}

}