namespace DataSyncHub.Shared.Infrastracture.Data.MongoDb
{
    public class MongoDbOptions
    {
        public string? ConnectionURI { get; set; }
        public string? DatabaseName { get; set; }
        public string? CollectionName { get; set; }
    }
}