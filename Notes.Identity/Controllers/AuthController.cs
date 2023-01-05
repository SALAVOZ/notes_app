using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Identity.Models;
using System.Threading.Tasks;

namespace Notes.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IIdentityServerInteractionService identityServerInteractionService)
        {
            _signInManager=signInManager;
            _userManager=userManager;
            _identityServerInteractionService=identityServerInteractionService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var ViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var user = await _userManager.FindByEmailAsync(viewModel.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User Not Found");
                return View(viewModel);
            }
            var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);
            
            if(result.Succeeded)
            {
                return Redirect(viewModel.ReturnUrl);
            }
            ModelState.AddModelError(string.Empty, "Login Error");
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var viewModel = new RegistryViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistryViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new AppUser
            {
                UserName = viewModel.Username
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }
            ModelState.AddModelError(string.Empty, "Error occured while registration");
            return View(viewModel);
        }

        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutResult = await _identityServerInteractionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutResult.PostLogoutRedirectUri);
        }
    }
}
