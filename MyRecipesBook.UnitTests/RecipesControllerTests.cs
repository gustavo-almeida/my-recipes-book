using System;
using Xunit;
using Moq;
using MyRecipesBook.Api.Repositories;
using MyRecipesBook.Api.Models;

namespace MyRecipesBook.UnitTests
{
    public class RecipesControllerTests
    {
        [Fact]
        public void GetRecipeAsync_WithUnexistingRecipe_ReturnsNotFound()
        {
            // Arrange
            //var repositoryStub = new Mock<IRecipesRepository>();

            //repositoryStub.Setup(async repo => repo.GetRecipeAsync(It.IsAny<Guid>()))
            //    .ReturnsAsync((Recipe)null);

            //var loggerStub = new Mock<Ilogger<RecipesController>>();
            
            // Act

            // Assert
        }
    }
}