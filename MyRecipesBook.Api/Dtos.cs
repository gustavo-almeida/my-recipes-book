using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyRecipesBook.Api.Dtos
{
    public record RecipeDto(Guid Id, string Title, List<string> Ingredients, List<string> Instructions, DateTimeOffset CreatedDate);
    public record CreateRecipeDto([Required] string Title, [Required] List<string> Ingredients, [Required] List<string> Instructions);
    public record UpdateRecipeDto([Required] string Title, [Required] List<string> Ingredients, [Required] List<string> Instructions);
}