using Blogss.Models;
using Blogss.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blogss.Controllers
{
    public class AuthorController(BlogsContext context) : Controller
    {
        private readonly AuthorsRepository authorsrepository = new AuthorsRepository(context);

        public IActionResult Index()
        {
            var authorList = authorsrepository.GetAuthors();

            if (TempData["SuccessMessage"] != null) 
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            }

            TempData.Clear();
            return View(authorList);
        }

        public IActionResult AddNewAuthor(Author author)
        { 
            var res = authorsrepository.AddAuthor(author); 
            
            if (res)
            {
                TempData["SuccessMessage"] = "Author has been added";
            }
            else
            {
                TempData["ErrorMessage"] = "Why you gay";
            }
            return RedirectToAction("Index");
        }
    }
}
