using GamingStore.Areas.Admin.Models;
using GamingStore.Data.Models;

namespace GamingStore.Areas.Admin.Services.Blogs
{
    public interface IBlogService
    {
        Task Create(BlogFormModel model);

        Task Delete(int id);

        Task Edit(BlogFormModel model, int id);

        Task<Blog> FindById(int id);

        Task<int> BlogsCount(string title);

        Task<IEnumerable<BlogViewModel>> GetAll();
    }
}
