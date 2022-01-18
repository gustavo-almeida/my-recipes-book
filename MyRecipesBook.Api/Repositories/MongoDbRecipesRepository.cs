using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MyRecipesBook.Api.Models;

namespace MyRecipesBook.Api.Repositories
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

        public async Task CreateRecipeAsync(Recipe recipe)
        {
            await recipesCollection.InsertOneAsync(recipe);
        }

        public async Task DeleteRecipeAsync(Guid id)
        {
            var filter = filterBuilder.Eq(recipe => recipe.Id, id);
            await recipesCollection.DeleteOneAsync(filter);
        }

        public async Task<Recipe> GetRecipeAsync(Guid id)
        {
            var filter = filterBuilder.Eq(recipe => recipe.Id, id);
            return await recipesCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync()
        {
            return await recipesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            var filter = filterBuilder.Eq(existingRecipe => existingRecipe.Id, recipe.Id);
            await recipesCollection.ReplaceOneAsync(filter, recipe);
        }
    }
}