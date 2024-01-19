using AcmeCorp.Data.Models;
using AcmeCorp.Repository.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace AcmeCorp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly IBlogsRepository _BlogsRepository;

        public BlogsController(IBlogsRepository BlogsRepository)
        {
            _BlogsRepository = BlogsRepository;
        }

        // GET: BlogsController
        public async Task<IActionResult> Index()
        {
            var Blogs = await _BlogsRepository.Get(true);
            return View(Blogs);
        }

        
        // GET: BlogsController/Create
        public async Task<IActionResult> Create()
        {
            string path = Directory.GetCurrentDirectory() + "/wwwroot/Content/Images";
            var images = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
                        .Select(f => new FileInfo(f)).ToArray();
            ViewBag.Images = images;
            return View();
        }

        // POST: BlogsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blogs model)
        {
            if (ModelState.IsValid)
            {
                model.CreationDate = DateTime.Now;
                if (releaseDateChecker(model.Active, model.ReleaseDate))
                    model.ReleaseDate = DateTime.Now;
                await _BlogsRepository.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: BlogsController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            string path = Directory.GetCurrentDirectory() + "/wwwroot/Content/Images";
            var images = Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories)
                        .Select(f => new FileInfo(f)).ToArray();
            ViewBag.Images = images;

            var BlogsItem = await _BlogsRepository.GetById(id);
            return View(BlogsItem);
        }

        // POST: BlogsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Blogs model)
        {
            if (ModelState.IsValid)
            {
                model.BlogsId = id;
                if (releaseDateChecker(model.Active, model.ReleaseDate))
                    model.ReleaseDate = DateTime.Now;
                await _BlogsRepository.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: BlogsController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var BlogsItem = await _BlogsRepository.GetById(id);
            return View(BlogsItem);
        }

        // POST: BlogsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _BlogsRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            //Get all blogs from the database

            List<Blogs> blogs = await _BlogsRepository.Get(false);

            string json = JsonConvert.SerializeObject(blogs);
            return Content(json, "application/json");

        }
        public bool releaseDateChecker(bool active, DateTime? blogReleaseDate)
        {
            //This will update the blogs release date to whenever they first made the blog active - checks active state on blog and checks if release date field on the blog already had a value. 
            if (active == true && blogReleaseDate == null)
                return true;
            return false;

        }




    }

}
