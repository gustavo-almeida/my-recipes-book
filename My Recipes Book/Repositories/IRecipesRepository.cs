using My_Recipes_Book.Models;
using System;
using System.Collections.Generic;

namespace My_Recipes_Book.Repositories
{
    public interface IRecipesRepository
    {
        Recipe GetRecipe(Guid id);
        IEnumerable<Recipe> GetRecipes();
        void CreateRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        void DeleteRecipe(Guid id);
    }
}