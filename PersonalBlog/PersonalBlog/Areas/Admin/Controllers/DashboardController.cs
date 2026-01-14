using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PersonalBlog.Areas.Admin
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult New()
        {
            return View("New");
        }

        [HttpPost]
        public IActionResult New(ArticleVm article) 
        {
            string taskFilePath = Path.Combine(AppContext.BaseDirectory + "article.txt");
            string json = JsonSerializer.Serialize(article);
            System.IO.File.WriteAllText(taskFilePath, json);
            return RedirectToAction("New");
        }
    }
}
