using Blogss.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogss.Repositories
{
    public class AuthorsRepository(BlogsContext _context) : IAuthorsRepository
    {
        private readonly BlogsContext context = _context;

        public bool AddAuthor(Author author)
        {
            try
            {
                author.DateCreated = DateTime.Now;
                context.Authors.Add(author);
                context.SaveChanges();
                return true;
            }catch (Exception) 
            {
                return false;
            }
        }

        public List<Author> GetAuthors()
        {
            return [..context.Authors];
            //var authors = context.Authors.ToList();
            //return authors ;
        }

        public bool RemoveAuthor(int authorId)
        {
            try
            {
                var author = context.Authors.Where(x=> x.Id == authorId).FirstOrDefault();
                if (author == null)
                {
                    return false;
                }

                context.Authors.Remove(author);
                context.SaveChanges();
                return true;

            }catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateAuthor(Author author)
        {
            try
            {
                context.Update(author);
                context.SaveChanges();

                return true;

            }catch (DbUpdateConcurrencyException)
            {
                if(context.Authors.Any(x=> x.Id == author.Id))
                {
                    throw;
                }
                return false;
            }
        }
    }
}
