using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDBSampleProject
{
    public class MongoCRUD
    {
        // Nuget package: MongoDB.Driver

        private IMongoDatabase database;

        public MongoCRUD(string databaseName)
        {
            var client = new MongoClient();
            database = client.GetDatabase(databaseName);
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = database.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = database.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string table, Guid id)
        {
            var collection = database.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            return collection.Find(filter).FirstOrDefault();
        }

        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = database.GetCollection<T>(table);
            collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new ReplaceOptions { IsUpsert = true }
               ); // new UpdateOptions { IsUpsert = true }
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = database.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }

    }
}
