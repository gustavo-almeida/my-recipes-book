using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyRecipesBook.Api.DTO
{
    public record UpdateRecipeDto
    {
        [Required]
        public string Title { get; init; }
        [Required]
        public List<string> Ingredients { get; init; }
        [Required]
        public List<string> Instructions { get; init; }
    }
}
