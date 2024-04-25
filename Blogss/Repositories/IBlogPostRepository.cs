using Blogss.Models;

namespace Blogss.Repositories
{
    public interface IBlogPostRepository
    {
        bool AddBlogPost(BlogPost blogPost);
        List<BlogPost> GetBlogPosts();
        bool RemoveBlogPost(int blogPostId);
        bool UpdateBlogPost(BlogPost blogPost);
    }
}