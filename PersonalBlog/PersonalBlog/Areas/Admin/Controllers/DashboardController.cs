using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PersonalBlog.Areas.Admin
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            List<ArticleVm> articles = new List<ArticleVm>();
            string taskFilePath = Path.Combine(AppContext.BaseDirectory + "article.txt");
            if(!System.IO.File.Exists(taskFilePath))
                System.IO.File.Create(taskFilePath).Close();
            string data = System.IO.File.ReadAllText(taskFilePath);
            if (!string.IsNullOrEmpty(data))
                articles.AddRange(JsonSerializer.Deserialize<List<ArticleVm>>(data)!);
            
            return View(articles);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View("New", new ArticleVm());
        }

        [HttpPost]
        public IActionResult New(ArticleVm article) 
        {
            List<ArticleVm> articles = new List<ArticleVm>();
            string taskFilePath = Path.Combine(AppContext.BaseDirectory + "article.txt");
            string data = System.IO.File.ReadAllText(taskFilePath);
            if (!string.IsNullOrEmpty(data))
                articles.AddRange(JsonSerializer.Deserialize<List<ArticleVm>>(data)!);
            int setupID = articles.Count > 0 ? articles.Max(x => x.Id) + 1 : 1;
            article.Id = setupID;
            articles.Add(article);
            string json = JsonSerializer.Serialize(articles);
            System.IO.File.WriteAllText(taskFilePath, json);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            List<ArticleVm> articles = new List<ArticleVm>();
            string taskFilePath = Path.Combine(AppContext.BaseDirectory + "article.txt");
            string data = System.IO.File.ReadAllText(taskFilePath);
            if (!string.IsNullOrEmpty(data))
                articles.AddRange(JsonSerializer.Deserialize<List<ArticleVm>>(data)!);

            ArticleVm? article = articles.FirstOrDefault(x => x.Id == id);
            if (article == null) return NotFound();

            articles.Remove(article);
            string json = JsonSerializer.Serialize(articles);
            System.IO.File.WriteAllText(taskFilePath, json);

            return RedirectToAction(nameof(Index));
        }
    }
}
