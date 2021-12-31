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
        public IEnumerable<RecipeDto> GetRecipes()
        {
            var recipes = repository.GetRecipes().Select(recipe => recipe.AsDto());
            return recipes;
        }

        [HttpGet("{id}")]
        public ActionResult<RecipeDto> GetRecipe(Guid id)
        {
            var recipe = repository.GetRecipe(id);

            if (recipe is null)
                return NotFound();

            return recipe.AsDto();
        }

        [HttpPost]
        public ActionResult<RecipeDto> CreateRecipe(CreateRecipeDto recipeDto)
        {
            Recipe recipe = new()
            {
                Id = Guid.NewGuid(),
                Title = recipeDto.Title,
                Ingredients = new List<string>(recipeDto.Ingredients),
                Instructions = new List<string>(recipeDto.Instructions)
            };

            repository.CreateRecipe(recipe);

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateRecipe(Guid id, UpdateRecipeDto recipeDto)
        {
            var existingRecipe = repository.GetRecipe(id);

            if (existingRecipe is null)
                return NotFound();

            Recipe updatedRecipe = existingRecipe with
            {
                Title = recipeDto.Title,
                Ingredients = new List<string>(recipeDto.Ingredients),
                Instructions = new List<string>(recipeDto.Instructions)
            };

            repository.UpdateRecipe(updatedRecipe);

            return NoContent();
        }
    }
}
