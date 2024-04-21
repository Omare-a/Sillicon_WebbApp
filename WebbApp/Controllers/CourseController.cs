using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebbApp.Models;

namespace WebbApp.Controllers;

[Authorize]
public class CourseController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;

	[Route("/courses")]
	public async Task<IActionResult> Index()
    {
        var viewModel = new CourseIndexViewModel();

        var respons = await _httpClient.GetAsync("https://localhost:7289/api/courses");
        if (respons.IsSuccessStatusCode)
        {
            var courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await respons.Content.ReadAsStringAsync());
            if(courses != null && courses.Any()) 
                viewModel.Courses = courses;
        }

        return View(viewModel);
    }
}
