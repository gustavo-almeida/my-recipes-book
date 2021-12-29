using Microsoft.AspNetCore.Mvc;
using My_Recipes_Book.DTO;
using My_Recipes_Book.Extensions;
using My_Recipes_Book.Models;
using My_Recipes_Book.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace My_Recipes_Book.Controllers
{
    [ApiController]
    [Route("recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesRepository repository;

        public RecipesController(IRecipesRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<RecipeDto> GetItems()
        {
            var recipes = repository.GetItems().Select(recipe => recipe.AsDto());
            return recipes;
        }

        [HttpGet("{id}")]
        public ActionResult<RecipeDto> GetItem(Guid id)
        {
            var recipe = repository.GetRecipe(id);

            if (recipe is null)
                return NotFound();

            return recipe.AsDto();
        }
    }
}
