using My_Recipes_Book.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Recipes_Book.Repositories
{
    public class InMemRecipesRepository : IRecipesRepository
    {
        private readonly List<Recipe> recipes = new()
        {
            new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Mousse de Maracujá fácil",
                Ingredients = new List<string>
                {
                    "2 caixas de gelatina de maracujá",
                    "2 xícaras de água quente",
                    "1 / 2 xícara de suco de maracujá",
                    "2 xícaras de leite de vaca",
                    "1 lata de leite condensado",
                    "1 lata de creme de leite sem soro"
                },
                Instructions = new List<string>
                {
                    "Prepare a gelatina com a água quente.",
                    "Depois é só bater todos os ingredientes no liquidificador e colocar numa travessa para gelar."
                }
            },
            new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Chocolate quente especial",
                Ingredients = new List<string>
                {
                    "1 litro de leite desnatado",
                    "1 lata de leite condensado",
                    "4 colheres (sopa) de chocolate em pó",
                    "2 colheres (sopa) de amido de milho",
                    "canela em pó para polvilhar"
                },
                Instructions = new List<string>
                {
                    "Bater todos os ingredientes no liquidificador (menos a canela).",
                    "Levar ao fogo em temperatura média até o líquido ferver engrossar.",
                    "Servir em xícaras, polvilhar com canela.",
                    "Para um sabor especial pode se colocar chantilly sobre a xícara de chocolate."
                }
            },
            new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Raspadinha de morango",
                Ingredients = new List<string>
                {
                    "1 lata de Leite Moça",
                    "1 vez a mesma medida de morango picado"
                },
                Instructions = new List<string>
                {
                    "Coloque no copo do liquidificador o Leite Moça.",
                    "Junte o morango ao Leite Moça.",
                    "Bata bem até que fique um creme consistente.",
                    "Encha 6 copos altos com o gelo picado.",
                    "Despeje o creme de morango sobre o gelo.",
                    "Coloque um canudinho e sirva a seguir."
                }
            }
        };

        public async Task<IEnumerable<Recipe>> GetRecipesAsync()
        {
            return await Task.FromResult(recipes);
        }

        public async Task<Recipe> GetRecipeAsync(Guid id)
        {
            var recipe = recipes.SingleOrDefault(recipe => recipe.Id == id);
            return await Task.FromResult(recipe);
        }

        public async Task CreateRecipeAsync(Recipe recipe)
        {
            recipes.Add(recipe);
            await Task.CompletedTask;
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            var index = recipes.FindIndex(existingRecipe => existingRecipe.Id == recipe.Id);
            recipes[index] = recipe;
            await Task.CompletedTask;
        }

        public async Task DeleteRecipeAsync(Guid id)
        {
            var index = recipes.FindIndex(existingRecipe => existingRecipe.Id == id);
            recipes.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}
