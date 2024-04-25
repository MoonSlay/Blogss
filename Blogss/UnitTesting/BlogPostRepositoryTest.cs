using Blogss.Models;
using Blogss.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Linq;

namespace Blogss.UnitTest
{
    [TestFixture]
    public class BlogPostRepositoryTest
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
        public void AddBlogPost()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new BlogPostRepository(context);
                var blogPost = new BlogPost { Title = "Test Post", Slug = "test-post", BlogContent = "Test Content", AuthorId = 1 }; // Assuming AuthorId 1 exists

                // Act
                var result = repository.AddBlogPost(blogPost);

                // Assert
                ClassicAssert.IsTrue(result);
            }
        }

        [Test]
        public void RemoveExistingBlogPost()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new BlogPostRepository(context);
                var blogPost = new BlogPost { Title = "Test Post", Slug = "test-post", BlogContent = "Test Content", AuthorId = 1 };
                repository.AddBlogPost(blogPost);

                // Act
                var result = repository.RemoveBlogPost(blogPost.Id);

                // Assert
                ClassicAssert.IsTrue(result);
            }
        }

        [Test]
        public void UpdateBlogPostTitle()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new BlogPostRepository(context);
                var blogPost = new BlogPost { Title = "Test Title", Slug = "test-post", BlogContent = "Test Content", AuthorId = 1 };
                repository.AddBlogPost(blogPost);
                blogPost.Title = "New title";

                // Act
                var result = repository.UpdateBlogPost(blogPost);

                // Assert
                ClassicAssert.IsTrue(result);
            }
        }

        [Test]
        public void UpdateBlogPostSlug()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new BlogPostRepository(context);
                var blogPost = new BlogPost { Title = "Test Title", Slug = "test-post", BlogContent = "Test Content", AuthorId = 1 };
                repository.AddBlogPost(blogPost);
                blogPost.Slug = "New post";

                // Act
                var result = repository.UpdateBlogPost(blogPost);

                // Assert
                ClassicAssert.IsTrue(result);
            }
        }

        [Test]
        public void UpdateBlogPostContent()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new BlogPostRepository(context);
                var blogPost = new BlogPost { Title = "Test Title", Slug = "test-post", BlogContent = "Test Content", AuthorId = 1 };
                repository.AddBlogPost(blogPost);
                blogPost.BlogContent = "New Content";

                // Act
                var result = repository.UpdateBlogPost(blogPost);

                // Assert
                ClassicAssert.IsTrue(result);
            }
        }

        [Test]
        public void ReturnNonEmptyListOfPostsAfterAdding()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new BlogPostRepository(context);
                var blogPost = new BlogPost { Title = "Test Post", Slug = "test-post", BlogContent = "Test Content", AuthorId = 1 };
                repository.AddBlogPost(blogPost);

                // Act
                var blogPosts = repository.GetBlogPosts();

                // Assert
                ClassicAssert.IsNotNull(blogPosts);
                ClassicAssert.AreNotEqual(0, blogPosts.Count); 
            }
        }

        [Test]
        public void ReturnCorrectPostAfterAdding()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new BlogPostRepository(context);
                var blogPost = new BlogPost { Title = "Test Post", Slug = "test-post", BlogContent = "Test Content", AuthorId = 1 };
                repository.AddBlogPost(blogPost);

                // Act
                var blogPosts = repository.GetBlogPosts();

                // Assert
                ClassicAssert.IsNotNull(blogPosts);
                ClassicAssert.AreEqual("Test Post", blogPosts.FirstOrDefault()?.Title);
            }
        }


        [Test]
        public void FalseIfPostAlreadyExists()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new BlogPostRepository(context);
                var blogPost = new BlogPost { Title = "Duplicate Post", Slug = "duplicate-post", BlogContent = "Duplicate Content", AuthorId = 1 };
                repository.AddBlogPost(blogPost);

                // Act
                var result = repository.AddBlogPost(blogPost);

                // Assert
                ClassicAssert.IsFalse(result);
            }
        }

        [Test]
        public void RemoveNonExistingBlogPost()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new BlogPostRepository(context);

                // Act
                var result = repository.RemoveBlogPost(999); // Non-existing blog post id

                // Assert
                ClassicAssert.IsFalse(result);
            }
        }

        [Test]
        public void DontAcceptEmptyTitle()
        {
            // Arrange
            using (var context = new BlogsContext(_options!))
            {
                var repository = new BlogPostRepository(context);
                var blogPost = new BlogPost { Title = null, Slug = "test-slug", BlogContent = "Lorem ipsum dolor sit amet", AuthorId = 1 };

                // Act
                var result = repository.AddBlogPost(blogPost);

                // Assert
                ClassicAssert.IsFalse(result);
            }
        }


    }
}
