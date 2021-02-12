using System;
using System.Collections.Generic;
using dotnet5todoapp.Models;
using dotnet5todoapp.Repositories;
using MongoDB.Driver;
using MongoDB.Bson;
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

        public void CreateTodo(TodoItem todoItem)
        {
            todosCollection.InsertOne(todoItem);
        }

        public void DeleteTodo(Guid id)
        {
            var filter = filterBuilder.Eq(todoItem => todoItem.Id, id);
            todosCollection.DeleteOne(filter);
        }

        public TodoItem GetTodo(Guid id)
        {
            var filter = filterBuilder.Eq(todoItem => todoItem.Id, id);
            return todosCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<TodoItem> GetTodos()
        {
            return todosCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateTodo(TodoItem todoItem)
        {
            var filter = filterBuilder.Eq(existingTodoItem => existingTodoItem.Id, todoItem.Id);
            todosCollection.ReplaceOne(filter, todoItem);
        }
    }
}