namespace DummyExtensions.MongoDB {

	public class BsonSettings {
		public static BsonSettings Defaults = new BsonSettings{ IgnoreDiscriminator = false, NormalizeId = false };

		public bool IgnoreDiscriminator { get; set; }
		public bool NormalizeId { get; set; }
	}

}