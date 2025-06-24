using Domus.Models;
using Domus.Models.Recipe;
using Domus.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domus.Controllers
{
    [Route("api/recipe")]
    [ApiController]
    public class RecipeController(RecipeService recipeService,
                                    UserService userService
    ) : DomusControllerBase<RecipeDto>(recipeService, userService)
    {
        public readonly RecipeService _recipeService = recipeService;
    }
}
