using MyRecipesBook.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRecipesBook.Api.Repositories
{
    public interface IRecipesRepository
    {
        Task<Recipe> GetRecipeAsync(Guid id);
        Task<IEnumerable<Recipe>> GetRecipesAsync();
        Task CreateRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(Guid id);
    }
}