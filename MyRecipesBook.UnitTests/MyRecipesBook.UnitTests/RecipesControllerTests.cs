using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MyRecipesBook.Api.Controllers;
using MyRecipesBook.Api.Models;
using MyRecipesBook.Api.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyRecipesBook.UnitTests
{
    public class RecipesControllerTests
    {
        [Fact]
        public async Task GetRecipeAsync_WithUnexistingRecipe_ReturnsNotFound()
        {
            // Arrange
            var repositoryStub = new Mock<IRecipesRepository>();

            repositoryStub.Setup(repo => repo.GetRecipeAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Recipe)null);

            var loggerStub = new Mock<ILogger<RecipesController>>();

            var controller = new RecipesController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetRecipeAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
