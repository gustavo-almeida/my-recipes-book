using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyRecipesBook.Api.Dtos;
using MyRecipesBook.Api.Extensions;
using MyRecipesBook.Api.Models;
using MyRecipesBook.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRecipesBook.Api.Controllers
{
    [ApiController]
    [Route("recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipesRepository repository;
        private readonly ILogger<RecipesController> logger;

        public RecipesController(IRecipesRepository repository, ILogger<RecipesController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<RecipeDto>> GetRecipesAsync(string title = null)
        {
            var recipes = (await repository.GetRecipesAsync()).Select(recipe => recipe.AsDto());

            if(!string.IsNullOrWhiteSpace(title))
            {
                recipes = recipes.Where(recipe => recipe.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }
            
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {recipes.Count()} recipes");
            
            return recipes;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetRecipeAsync(Guid id)
        {
            var recipe = await repository.GetRecipeAsync(id);

            if (recipe is null)
                return NotFound();

            return recipe.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<RecipeDto>> CreateRecipeAsync(CreateRecipeDto recipeDto)
        {
            Recipe recipe = new()
            {
                Id = Guid.NewGuid(),
                Title = recipeDto.Title,
                Ingredients = new List<string>(recipeDto.Ingredients),
                Instructions = new List<string>(recipeDto.Instructions),
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateRecipeAsync(recipe);

            return CreatedAtAction(nameof(GetRecipeAsync), new { id = recipe.Id }, recipe.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRecipeAsync(Guid id, UpdateRecipeDto recipeDto)
        {
            var existingRecipe = await repository.GetRecipeAsync(id);

            if (existingRecipe is null)
                return NotFound();

            existingRecipe.Title = recipeDto.Title;
            existingRecipe.Ingredients = new List<string>(recipeDto.Ingredients);
            existingRecipe.Instructions = new List<string>(recipeDto.Instructions);

            await repository.UpdateRecipeAsync(existingRecipe);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task <ActionResult> DeleteRecipeAsync(Guid id)
        {
            var existingRecipe = await repository.GetRecipeAsync(id);

            if (existingRecipe is null)
                return NotFound();

            await repository.DeleteRecipeAsync(id);

            return NoContent();
        }
    }
}
