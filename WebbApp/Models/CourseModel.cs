﻿namespace WebbApp.Models;

public class CourseModel
{
	public int Id { get; set; }
	public bool IsBestSeller { get; set; }
	public string Image { get; set; } = null!;
	public string Title { get; set; } = null!;
	public string Author { get; set; } = null!;
	public string Price { get; set; } = null!;
	public string? DiscountPrice { get; set; }
	public string Hours { get; set; } = null!;
	public string Rating { get; set; } = null!;
	public string Likes { get; set; } = null!;
}

public class CourseIndexViewModel
{
	public IEnumerable<CourseModel> Courses { get; set; } = [];
}