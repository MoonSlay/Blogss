using Blogss.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogss.Repositories
{
    public class BlogPostRepository(BlogsContext context) : IBlogPostRepository
    {
        private readonly BlogsContext _context = context;

        public List<BlogPost> GetAllBlogPost()
        {
            return _context.BlogPosts.ToList();
        }


        public bool AddBlogPost(BlogPost blogPost)
        {
            try
            {
                _context.BlogPosts.Add(blogPost);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<BlogPost> GetBlogPosts()
        {
            return _context.BlogPosts.Include(b => b.Author).ToList();
        }

        public bool RemoveBlogPost(int blogPostId)
        {
            try
            {
                var blogPost = _context.BlogPosts.Find(blogPostId);
                if (blogPost == null)
                {
                    return false;
                }

                _context.BlogPosts.Remove(blogPost);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateBlogPost(BlogPost blogPost)
        {
            try
            {
                _context.Update(blogPost);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.BlogPosts.Any(x => x.Id == blogPost.Id))
                {
                    throw;
                }
                return false;
            }
        }
    }
}
