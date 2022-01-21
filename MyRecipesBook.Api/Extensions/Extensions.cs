using MyRecipesBook.Api.Dtos;
using MyRecipesBook.Api.Models;
using System.Collections.Generic;

namespace MyRecipesBook.Api.Extensions
{
    public static class Extensions
    {
        public static RecipeDto AsDto(this Recipe recipe)
        {
            return new RecipeDto(recipe.Id, recipe.Title, recipe.Ingredients, recipe.Instructions, recipe.CreatedDate);
        }
    }
}
