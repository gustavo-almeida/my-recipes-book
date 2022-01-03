using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using My_Recipes_Book.Models;

namespace My_Recipes_Book.Repositories
{
    public class MongoDbRecipesRepository : IRecipesRepository
    {
        private const string databaseName = "recipesBookDB";
        private const string collectionName = "recipes";
        private readonly IMongoCollection<Recipe> recipesCollection;
        private readonly FilterDefinitionBuilder<Recipe> filterBuilder = Builders<Recipe>.Filter;
        
        public MongoDbRecipesRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            recipesCollection = database.GetCollection<Recipe>(collectionName);
        }

        public void CreateRecipe(Recipe recipe)
        {
            recipesCollection.InsertOne(recipe);
        }

        public void DeleteRecipe(Guid id)
        {
            var filter = filterBuilder.Eq(recipe => recipe.Id, id);
            recipesCollection.DeleteOne(filter);
        }

        public Recipe GetRecipe(Guid id)
        {
            var filter = filterBuilder.Eq(recipe => recipe.Id, id);
            return recipesCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Recipe> GetRecipes()
        {
            return recipesCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            var filter = filterBuilder.Eq(existingRecipe => existingRecipe.Id, recipe.Id);
            recipesCollection.ReplaceOne(filter, recipe);
        }
    }
}