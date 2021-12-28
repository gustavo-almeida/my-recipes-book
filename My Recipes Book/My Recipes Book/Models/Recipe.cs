﻿using System;
using System.Collections.Generic;

namespace My_Recipes_Book.Models
{
    public record Recipe
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public List<string> Ingredients { get; init; }
        public List<string> Instructions { get; init; }
    }
}