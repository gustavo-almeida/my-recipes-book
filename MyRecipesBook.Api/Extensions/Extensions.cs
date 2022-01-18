using MyRecipesBook.Api.DTO;
using MyRecipesBook.Api.Models;
using System.Collections.Generic;

namespace MyRecipesBook.Api.Extensions
{
    public static class Extensions
    {
        public static RecipeDto AsDto(this Recipe recipe)
        {
            return new RecipeDto
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Ingredients = new List<string>(recipe.Ingredients),
                Instructions = new List<string>(recipe.Instructions)
            };
        }
    }
}
