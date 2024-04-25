using Blogss.Models;
using Blogss.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogss.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public IActionResult Index()
        {
            var blogPosts = _blogPostRepository.GetBlogPosts();
            return View(blogPosts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                blogPost.Id = 1; 

                if (_blogPostRepository.AddBlogPost(blogPost))
                {
                    TempData["SuccessMessage"] = "Blog post created!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to create blog post.";
                }
            }
            return View(blogPost);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = _blogPostRepository.GetBlogPosts().FirstOrDefault(x => x.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _blogPostRepository.UpdateBlogPost(blogPost);
                    TempData["SuccessMessage"] = "Blog post updated!";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Blog post may have been modified by another user.");
                    return View(blogPost);
                }
            }
            return View(blogPost);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = _blogPostRepository.GetBlogPosts().FirstOrDefault(x => x.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPost/Delete/5
        [HttpPost, ActionName("Delete")] // Use ActionName to differentiate from GET
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_blogPostRepository.RemoveBlogPost(id))
            {
                TempData["SuccessMessage"] = "Blog post deleted!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete blog post.";
                return RedirectToAction("Delete", new { id = id }); // Re-display with error
            }
        }
    }
}
