using System;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace DummyExtensions {

	public static class mongo_instance_extension {
		public static Statements.Statements from_collection( this MongoDatabase database, string collection_name ) {
			var query_settings = new QuerySettings();
			query_settings.CollectionName = collection_name;
			query_settings.MongoDatabase = database;
			
			return new Statements.Statements( query_settings );
		}
		
		public static Into insert( this MongoDatabase database, object document ) {
			var query_settings = new QuerySettings();
			query_settings.MongoDatabase = database;
			query_settings.Document = document;
			
			return new Into( query_settings );
		}

		public static Update update( this MongoDatabase database, Guid id ) {
			var query_settings = new QuerySettings();
			query_settings.MongoDatabase = database;
			query_settings.Id = id;
			
			return new Update( query_settings );
		}
	}

}