using Blogss.Models;
using Blogss.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Blogss.UnitTest
{
    [TestFixture]
    public class AuthorRepositoryTest
    {
        private DbContextOptions<BlogsContext>? _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<BlogsContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;
        }


        [Test]
        public void AddNewAuthor()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new AuthorsRepository(context);
                var author = new Author { Name = "Test Author", Email = "test@example.com", DateCreated = DateTime.Now };

                // Act
                var result = repository.AddAuthor(author);

                // Assert
                ClassicAssert.IsTrue(result);
            }
        }

        [Test]
        public void FalseIfEmptyAuthor()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new AuthorsRepository(context);

                // Act
                var result = repository.AddAuthor(null);

                // Assert
                ClassicAssert.IsFalse(result);
            }
        }

        [Test]
        public void FalseIfAuthorExists()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new AuthorsRepository(context);
                var author = new Author { Name = "Duplicate Author", Email = "duplicate@example.com", DateCreated = DateTime.Now };
                repository.AddAuthor(author);

                // Act
                var result = repository.AddAuthor(author);

                // Assert
                ClassicAssert.IsFalse(result);
            }
        }


        [Test]
        public void RemoveExistingAuthor()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new AuthorsRepository(context);
                var author = new Author { Name = "Test Author", Email = "test@example.com", DateCreated = DateTime.Now };
                repository.AddAuthor(author);

                // Act
                var result = repository.RemoveAuthor(author.Id);

                // Assert
                ClassicAssert.IsTrue(result);
            }
        }

        [Test]
        public void RemoveNonExistingAuthor()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new AuthorsRepository(context);

                // Act
                var result = repository.RemoveAuthor(999); // Non-existing author id

                // Assert
                ClassicAssert.IsFalse(result);
            }
        }

        [Test]
        public void UpdateAuthorsName()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new AuthorsRepository(context);
                var author = new Author { Name = "Test Author", Email = "test@example.com", DateCreated = DateTime.Now };
                repository.AddAuthor(author);
                author.Name = "Updated Author";

                // Act
                var result = repository.UpdateAuthor(author);

                // Assert
                ClassicAssert.IsTrue(result);
            }
        }

        [Test]
        public void UpdateAuthorsEmailAddress()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new AuthorsRepository(context);
                var author = new Author { Name = "Test Author", Email = "test@example.com", DateCreated = DateTime.Now };
                repository.AddAuthor(author);

                // Update author's email
                author.Email = "updated@example.com";

                // Act
                var result = repository.UpdateAuthor(author);

                // Assert
                ClassicAssert.IsTrue(result);
            }
        }

        [Test]
        public void UpdateNonExistingAuthor()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new AuthorsRepository(context);
                var author = new Author { Id = 999, Name = "Test Author", Email = "test@example.com", DateCreated = DateTime.Now }; // Non-existing author id

                // Act
                var result = repository.UpdateAuthor(author);

                // Assert
                ClassicAssert.IsFalse(result);
            }
        }

        [Test]
        public void ReturnNonEmptyListOfAuthorsAfterAdding()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new AuthorsRepository(context);
                var author = new Author { Name = "Test Author", Email = "test@example.com", DateCreated = DateTime.Now };
                repository.AddAuthor(author);

                // Act
                var authors = repository.GetAuthors();

                // Assert
                ClassicAssert.IsNotNull(authors);
                ClassicAssert.AreNotEqual(0, authors.Count); // Authors should be added
            }
        }

        [Test]
        public void ReturnCorrectAuthorAfterAdding()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new AuthorsRepository(context);
                var author = new Author { Name = "Test Author", Email = "test@example.com", DateCreated = DateTime.Now };
                repository.AddAuthor(author);

                // Act
                var authors = repository.GetAuthors();

                // Assert
                ClassicAssert.IsNotNull(authors);
                ClassicAssert.AreEqual("Test Author", authors.FirstOrDefault()?.Name); // First author should be "Test Author"
            }
        }


    }
}
