using System;
using System.Collections.Generic;
using dotnet5todoapp.Models;
using dotnet5todoapp.Repositories;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
namespace dotnet5todoapp
{
    public class MongoDBTodoRepository : ITodosRepository
    {
        private const String CollectionName = "todos";
        private const String DatabaseName = "todoDB";

        private readonly FilterDefinitionBuilder<TodoItem> filterBuilder = Builders<TodoItem>.Filter;

        private readonly IMongoCollection<TodoItem> todosCollection;

        public MongoDBTodoRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(DatabaseName);
            todosCollection = database.GetCollection<TodoItem>(CollectionName);
        }

        public async Task CreateTodoAsync(TodoItem todoItem)
        {
            await todosCollection.InsertOneAsync(todoItem);
        }

        public async Task DeleteTodoAsync(Guid id)
        {
            var filter = filterBuilder.Eq(todoItem => todoItem.Id, id);
            await todosCollection.DeleteOneAsync(filter);
        }

        public async Task<TodoItem> GetTodoAsync(Guid id)
        {
            var filter = filterBuilder.Eq(todoItem => todoItem.Id, id);
            return await todosCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetTodosAsync()
        {
            return await todosCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateTodoAsync(TodoItem todoItem)
        {
            var filter = filterBuilder.Eq(existingTodoItem => existingTodoItem.Id, todoItem.Id);
            await todosCollection.ReplaceOneAsync(filter, todoItem);
        }
    }
}