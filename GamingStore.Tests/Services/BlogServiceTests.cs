using GamingStore.Areas.Admin.Models;
using GamingStore.Areas.Admin.Services.Blogs;
using GamingStore.Data;
using GamingStore.Data.Models;
using GamingStore.Tests.Mocks;
using Xunit;

namespace GamingStore.Tests.Services
{
    public class BlogServiceTests
    {
        private GamingStoreDbContext data;
        private IBlogService blogService;

        public BlogServiceTests()
        {
            this.data = DatabaseMock.Instance;

            this.data.Blogs.Add(new Blog
            {
                Title = "Title",
                Content = "Content",
                ImageUrl = "ImageUrl"
            });

            this.data.SaveChanges();

            this.blogService = new BlogService(this.data, MapperMock.Instance);
        }

        [Fact]
        public async Task CreateBlogShouldAddTheBlogSuccessfully()
        {
            await this.blogService.Create(new BlogFormModel
            {
                Title = "Title",
                Content = "Content",
                ImageUrl = "ImageUrl"
            });

            Assert.Equal(2, this.data.Blogs.Count());
        }

        [Fact]
        public async Task DeleteShouldRemoveTheBlogSuccessfully()
        {
            await this.blogService.Delete(1);

            Assert.Empty(this.data.Blogs);
        }
    }
}
