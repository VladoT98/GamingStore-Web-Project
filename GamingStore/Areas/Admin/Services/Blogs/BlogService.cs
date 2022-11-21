using AutoMapper;
using AutoMapper.QueryableExtensions;
using GamingStore.Areas.Admin.Models;
using GamingStore.Data;
using GamingStore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GamingStore.Areas.Admin.Services.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly GamingStoreDbContext data;
        private readonly IMapper mapper;

        public BlogService(GamingStoreDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task CreateBlog(BlogFormModel model)
        {
            var blog = mapper.Map<Blog>(model);

            await data.Blogs.AddAsync(blog);
            await data.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var blog = await data.Blogs.FindAsync(id);

            data.Blogs.Remove(blog);
            await data.SaveChangesAsync();
        }

        public async Task Edit(BlogFormModel model, int id)
        {
            var blog = await data.Blogs.FindAsync(id);

            blog.Title = model.Title;
            blog.ImageUrl = model.ImageUrl;
            blog.Content = model.Content;

            await data.SaveChangesAsync();
        }

        public async Task<Blog> FindById(int id)
            => await data.Blogs.FindAsync(id);

        public async Task<int> BlogsCount(string title)
            => string.IsNullOrEmpty(title)
                ? await data.Blogs.CountAsync()
                : await data.Blogs.CountAsync(x => x.Title.ToLower().Contains(title.ToLower()));

        public async Task<IEnumerable<BlogViewModel>> GetAll()
            => await data.Blogs
                .ProjectTo<BlogViewModel>(mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
