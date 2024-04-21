using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebbApp.Models;

namespace WebbApp.Controllers;

public class DefaultController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;

    public IActionResult Home()
    {
        TempData["StatusMassage"] = "* Yes, I agree to the terms and privacy policy.";
        return View();
    }

    public async Task<IActionResult> Subscribe(SubscribeModel model)
    {
        if (ModelState.IsValid)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var respons = await _httpClient.PostAsync("https://localhost:7289/api/subscribe", content);
            if (respons.IsSuccessStatusCode)
            {
                TempData["StatusMassage"] = "You are now subscribed";
            }
            else if(respons.StatusCode == System.Net.HttpStatusCode.Conflict) 
            {
                TempData["StatusMassage"] = "You are already subscribed";
            }
        }
        else
        {
            TempData["StatusMassage"] = "Invalid email adress";

        }

        return RedirectToAction("Home", "Default", "subscribe");
    }
}
