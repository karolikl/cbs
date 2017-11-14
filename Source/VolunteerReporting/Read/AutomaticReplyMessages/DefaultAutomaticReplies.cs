using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Events.External;

namespace Read.AutomaticReplyMessages
{
    public class DefaultAutomaticReplies : IDefaultAutomaticReplies
    {
        readonly IMongoDatabase _database;
        readonly IMongoCollection<DefaultAutomaticReply> _collection;

        public DefaultAutomaticReplies(IMongoDatabase database)
        {
            _database = database;
            _collection = database.GetCollection<DefaultAutomaticReply>("DefaultAutomaticReply");
        }

        public DefaultAutomaticReply GetByTypeAndLanguage(DefaultAutomaticReplyType type, string language)
        {
            var filter = Builders<DefaultAutomaticReply>.Filter.Where(v => v.Type == type && v.Language == language);
            return _collection.Find(filter).FirstOrDefault();
        }

        public void Save(DefaultAutomaticReply automaticReply)
        {
            var filter = Builders<DefaultAutomaticReply>.Filter.Where(v => v.Type == automaticReply.Type && v.Language == automaticReply.Language);
            _collection.ReplaceOne(filter, automaticReply, new UpdateOptions { IsUpsert = true });
        }
    }
}
