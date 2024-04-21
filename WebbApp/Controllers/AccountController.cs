using Infrastructur.Context;
using Infrastructur.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebbApp.Models;
using WebbApp.ViewModels;

namespace WebbApp.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, DataContext dataContext) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly DataContext _dataContext = dataContext;

    public async Task<IActionResult> Details()
    {
        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = await _dataContext.Users.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == nameIdentifier);

        var model = new AccountDetailViewModel
        {
            BasicInfo = new AccountDetailsBasicInfoModel
            {
                FirstName = user!.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                Bio = user.Bio,
            },
            AddressInfo = new AccountDetailsAddressInfoModel
            {
                AddressLineOne = user.Address?.AddressLine_1!,
                AddressLineTwo = user.Address?.AddressLine_2!,
                PostalCode = user.Address?.PostalCode!,
                City = user.Address?.City!,
            }
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBasicInfo(AccountDetailViewModel model)
    {
        ModelState.Clear();
        if (TryValidateModel(model.BasicInfo!))
        {
            var user = await _userManager.GetUserAsync(User);
            //om den finns
            if(user != null)
            {
                user.FirstName = model.BasicInfo!.FirstName;
                user.LastName = model.BasicInfo!.LastName;
                user.Email = model.BasicInfo!.Email;
                user.PhoneNumber = model.BasicInfo!.PhoneNumber;
                user.UserName = model.BasicInfo!.Email;
                user.Bio = model.BasicInfo!.Bio;


                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["StatusMassage"] = "Personal information is saved.";

                }
                else
                {
                    TempData["StatusMassage"] = "Unable to save personal information.";

                }
    
            }
        }
        else
        {
            TempData["StatusMassage"] = "Unable to save personal information.";
        }

        return RedirectToAction("Details", "Account");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAddressInfo(AccountDetailViewModel model)
    {
        ModelState.Clear();
        if (TryValidateModel(model.AddressInfo!))
        {
            var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var user = await _dataContext.Users.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == nameIdentifier);

            //den e klurig
            if (user != null)
            {
                try
                {
                    if (user.Address != null)
                    {
                        user.Address.AddressLine_1 = model.AddressInfo!.AddressLineOne;
                        user.Address.AddressLine_2 = model.AddressInfo.AddressLineTwo ?? " ";
                        user.Address.PostalCode = model!.AddressInfo!.PostalCode;
                        user.Address.City = model.AddressInfo!.City;
                    }
                    else
                    {
                        user.Address = new AdressEntity
                        {
                            AddressLine_1 = model.AddressInfo!.AddressLineOne,
                            AddressLine_2 = model.AddressInfo.AddressLineTwo ?? " ",
                            PostalCode = model.AddressInfo!.PostalCode,
                            City = model.AddressInfo!.City,

                        };
                    }

                    _dataContext.Update(user);
                    await _dataContext.SaveChangesAsync();

                    TempData["StatusMassage"] = "Address information is saved.";
                }
                catch 
                {
                    TempData["StatusMassage"] = "Unable save Address information.";
                }

            }
        }
        else
        {
            TempData["StatusMassage"] = "Unable save Address information.";
        }

        return RedirectToAction("Details", "Account");
    }

    [HttpPost]
    public async Task<IActionResult> UploadProfileImage(IFormFile file)
    {
        var user = await _userManager.GetUserAsync(User);
        if(user != null && file != null && file.Length != 0)
        {
            var fileName = $"p_{user.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/uploads/profiles", fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            user.ProfileImage = fileName;
            await _userManager.UpdateAsync(user);

		}
		else
        {
            TempData["StatusMassage"] = "Unable to upload profile image.";
        }


		return RedirectToAction("Details", "Account");
	}
}
