using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MyRecipesBook.Api.Controllers;
using MyRecipesBook.Api.Dtos;
using MyRecipesBook.Api.Models;
using MyRecipesBook.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MyRecipesBook.UnitTests
{
    public class RecipesControllerTests
    {
        private readonly Mock<IRecipesRepository> repositoryStub = new();
        private readonly Mock<ILogger<RecipesController>> loggerStub = new();

        [Fact]
        public async Task GetRecipeAsync_WithUnexistingRecipe_ReturnsNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetRecipeAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Recipe)null);

            var controller = new RecipesController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetRecipeAsync(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetRecipeAsync_WithExistingRecipe_ReturnsExpectedRecipe()
        {
            // Arrange
            var expectedRecipe = CreateRandomRecipe();

            repositoryStub.Setup(repo => repo.GetRecipeAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedRecipe);

            var controller = new RecipesController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetRecipeAsync(Guid.NewGuid());

            // Assert
            result.Value.Should().BeEquivalentTo(expectedRecipe);
        }

        [Fact]
        public async Task GetRecipesAsync_WithExistingRecipes_ReturnsAllRecipes()
        {
            // Arrange
            var expectedRecipes = new[]{CreateRandomRecipe(), CreateRandomRecipe(), CreateRandomRecipe()};

            repositoryStub.Setup(repo => repo.GetRecipesAsync())
                .ReturnsAsync(expectedRecipes);

            var controller = new RecipesController(repositoryStub.Object, loggerStub.Object);

            // Act
            var actualRecipes = await controller.GetRecipesAsync();

            // Assert
            actualRecipes.Should().BeEquivalentTo(expectedRecipes);
        }

        [Fact]
        public async Task GetRecipesAsync_WithMatchingRecipes_ReturnsMatchingRecipes()
        {
            // Arrange
            var allRecipes = new[]
            {
                new Recipe(){ Title = "Sorvete com Banana" },
                new Recipe(){ Title = "Uva passa" },
                new Recipe(){ Title = "Banana split" }
            };

            var titleToMatch = "Banana";

            repositoryStub.Setup(repo => repo.GetRecipesAsync())
                .ReturnsAsync(allRecipes);

            var controller = new RecipesController(repositoryStub.Object, loggerStub.Object);

            // Act
            IEnumerable<RecipeDto> foundRecipes = await controller.GetRecipesAsync(titleToMatch);

            // Assert
            foundRecipes.Should().OnlyContain(
                recipe => recipe.Title == allRecipes[0].Title || recipe.Title == allRecipes[2].Title
            );
        }

        [Fact]
        public async Task CreateRecipeAsync_WithRecipeToCreate_ReturnsCreatedRecipe()
        {
            // Arrange
            var recipeToCreate = new CreateRecipeDto
            (
                Guid.NewGuid().ToString(),
                new List<string>{ Guid.NewGuid().ToString(),Guid.NewGuid().ToString() },
                new List<string>{ Guid.NewGuid().ToString(),Guid.NewGuid().ToString() }
            );

            var controller = new RecipesController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.CreateRecipeAsync(recipeToCreate);

            // Assert
            var createdRecipe = (result.Result as CreatedAtActionResult).Value as RecipeDto;
            recipeToCreate.Should().BeEquivalentTo(
                createdRecipe,
                options => options.ComparingByMembers<RecipeDto>().ExcludingMissingMembers()
            );
            createdRecipe.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UpdateRecipeAsync_WithExistingItem_ReturnsNoContent()
        {
            // Arrange
            var existingRecipe = CreateRandomRecipe();

            repositoryStub.Setup(repo => repo.GetRecipeAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingRecipe);

            var recipeId = existingRecipe.Id;
            var recipeToUpdate = new UpdateRecipeDto(
                Guid.NewGuid().ToString(),
                new List<string>{ Guid.NewGuid().ToString() },
                new List<string>{ Guid.NewGuid().ToString() }
            );

            var controller = new RecipesController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.UpdateRecipeAsync(recipeId, recipeToUpdate);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteRecipeAsync_WithExistingItem_ReturnsNoContent()
        {
            // Arrange
            var existingRecipe = CreateRandomRecipe();

            repositoryStub.Setup(repo => repo.GetRecipeAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingRecipe);

            var controller = new RecipesController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.DeleteRecipeAsync(existingRecipe.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        private Recipe CreateRandomRecipe()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Title = Guid.NewGuid().ToString(),
                Ingredients = new List<string>
                { 
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString() 
                },
                Instructions = new List<string>
                { 
                    Guid.NewGuid().ToString(), 
                    Guid.NewGuid().ToString() 
                }
            };
        }
    }
}
