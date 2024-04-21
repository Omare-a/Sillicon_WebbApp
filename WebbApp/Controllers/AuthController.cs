using Infrastructur.Context;
using Infrastructur.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebbApp.ViewModels;

namespace WebbApp.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, DataContext dataContext) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly DataContext _dataContext = dataContext;

    [Route("/signup")]
    [HttpGet]
    public IActionResult SignUp()
    {
        var model = new SignUpViewModel();
        return View(model);
    }

    [Route("/signup")]
    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpViewModel model)
    {
        if (ModelState.IsValid)
        {
            var anyAsync = await _dataContext.Users.AnyAsync(x => x.Email == model.Form.Email);
            if (!anyAsync)
            {
                //map
                var userEntity = new UserEntity
                {
                    Email = model.Form.Email,
                    UserName = model.Form.Email,
                    FirstName = model.Form.FirstName,
                    LastName = model.Form.LastName,
                };

                var result = await _userManager.CreateAsync(userEntity, model.Form.Password);
                if (result.Succeeded)
                {
                    var signIn = await _signInManager.PasswordSignInAsync(model.Form.Email, model.Form.Password, false, false);
                    if (signIn.Succeeded)
                        return LocalRedirect("/");
                    else
                        return LocalRedirect("/signin");
                }
                else
                {
                    model.ErrorMassage = "Something went wrong. Try agian later or contact customer service";
                }

            }
            else
            {
                model.ErrorMassage = "User with the same email already exist";
            }
            
        }

        return View(model);
    }

    [Route("/signin")]
    [HttpGet]
    public IActionResult SignIn(string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl ?? "/";
        var model = new SignInViewModel();
        return View(model);
    }

    [Route("/signin")]
    [HttpPost]
    public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Form.Email, model.Form.Password, model.Form.RememberMe, false);
            if (result.Succeeded)
                return LocalRedirect(returnUrl);

        }

        ViewData["ReturnUrl"] = returnUrl;
        model.ErrorMassage = "Incorrect email or passwors";
        return View(model);
    }

    [Route("/signout")]
    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Home", "Default");
    }

}
