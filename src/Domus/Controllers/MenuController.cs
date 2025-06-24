using Domus.Models;
using Domus.Models.Menu;
using Domus.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Domus.Controllers
{
    [Route("api/menu")]
    [ApiController]
    public class MenuController(MenuService menuService,
                                UserService userService
    ) : DomusControllerBase<MenuDto>(menuService, userService)
    {
        private readonly MenuService _menuService = menuService;
    }
}
