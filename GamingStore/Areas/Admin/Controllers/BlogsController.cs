using AutoMapper;
using GamingStore.Areas.Admin.Models;
using GamingStore.Areas.Admin.Services.Blogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IMapper mapper;

        public BlogsController(IBlogService blogService, IMapper mapper)
        {
            this.blogService = blogService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
            => View();

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Create(BlogFormModel model)
        {
            await this.blogService.CreateBlog(model);
            return RedirectToAction(nameof(AdminController.AdminBlogs), "Admin");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            var blogForm = mapper.Map<BlogFormModel>(await this.blogService.FindById(id));
            return View(blogForm);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Edit(BlogFormModel model, int id)
        {
            await this.blogService.Edit(model, id);
            return RedirectToAction(nameof(AdminController.AdminBlogs), "Admin");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.blogService.Delete(id);
            return RedirectToAction(nameof(AdminController.AdminBlogs), "Admin");
        }

        public async Task<IActionResult> Details(int id)
            => View(await this.blogService.FindById(id));

        public async Task<IActionResult> All()
            => View(await this.blogService.GetAll());
    }
}