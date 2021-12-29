using My_Recipes_Book.DTO;
using My_Recipes_Book.Models;
using System.Collections.Generic;

namespace My_Recipes_Book.Extensions
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
