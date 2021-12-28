using Microsoft.AspNetCore.Mvc;
using My_Recipes_Book.Models;
using My_Recipes_Book.Repositories;
using System;
using System.Collections.Generic;

namespace My_Recipes_Book.Controllers
{
    [ApiController]
    [Route("recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly InMemRecipesRepository repository;

        public RecipesController()
        {
            repository = new InMemRecipesRepository();
        }

        [HttpGet]
        public IEnumerable<Recipe> GetItems()
        {
            var recipes = repository.GetItems();
            return recipes;
        }

        [HttpGet("{id}")]
        public ActionResult<Recipe> GetItem(Guid id)
        {
            var recipe = repository.GetRecipe(id);

            if (recipe is null)
                return NotFound();

            return recipe;
        }
    }
}
