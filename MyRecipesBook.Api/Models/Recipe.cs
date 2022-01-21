using System;
using System.Collections.Generic;

namespace MyRecipesBook.Api.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Instructions { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}