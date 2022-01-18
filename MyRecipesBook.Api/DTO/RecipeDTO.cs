using System;
using System.Collections.Generic;

namespace MyRecipesBook.Api.DTO
{
    public record RecipeDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public List<string> Ingredients { get; init; }
        public List<string> Instructions { get; init; }
    }
}
