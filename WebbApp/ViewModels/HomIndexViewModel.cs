using WebApp.Models.Sections;

namespace WebApp.Models.Views;

public class HomIndexViewModel
{
    public string Title { get; set; } = "Ultimate Task Management Assistant";
    public ShowcaseViewModel Showcase { get; set; } = new ShowcaseViewModel
	{
		Id = "overview",
		ShowcaseImage = new() { ImgUrl = "images/showcase-taskmaster.svg", AltText = "Task Management Assistant" },
		Title = "Task Management Assistant You Gonna Love ",
		Text = "We offer you a new generation of task management system. Plan, manage & track all your tasks in one flexible tool.\r\n",
		Link = new() { ControllerName = "Downloads", ActionName = "Index", Text = "Ger started for free" },
		BrandDescription = "Largest companies use our tool to work efficiently",
		Brands =
		[
			new () { ImgUrl = "images/brands/brand_1.svg", AltText = "Brand Name 1"},
			new () { ImgUrl = "images/brands/brand_2.svg", AltText = "Brand Name 2"},
			new () { ImgUrl = "images/brands/brand_3.svg", AltText = "Brand Name 3"},
			new () { ImgUrl = "images/brands/brand_4.svg", AltText = "Brand Name 4"},

		],
	};
}
