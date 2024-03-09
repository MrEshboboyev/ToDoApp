using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDo.Web.Models;
using ToDo.Web.Service.IService;
using ToDo.Web.Utility;

namespace ToDo.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService; 
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem(){Text = SD.RoleAdmin, Value = SD.RoleAdmin},
                new SelectListItem(){Text = SD.RoleCustomer, Value = SD.RoleCustomer}
            };

            ViewBag.RoleList = roleList;

            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDto result = await _authService.RegisterAsync(obj);
            ResponseDto assignRole;

            if (result != null && result.IsSuccess)
            {
                if(string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleCustomer;
                }
                assignRole = await _authService.AssignRoleAsync(obj);
                if(assignRole != null && assignRole.IsSuccess)
                {
                    TempData["success"] = "Registration Successfully!";
                    return RedirectToAction(nameof(Login));
                }
            }

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem(){Text = SD.RoleAdmin, Value = SD.RoleAdmin},
                new SelectListItem(){Text = SD.RoleCustomer, Value = SD.RoleCustomer}
            };

            ViewBag.RoleList = roleList;

            return View(obj);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
