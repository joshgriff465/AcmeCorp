using AcmeCorp.Data.Models;
using AcmeCorp.Repository.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcmeCorp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly INewsRepository _NewsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _NewsRepository = newsRepository;
        }

        // GET: NewsController
        public async Task<IActionResult> Index()
        {
            var news = await _NewsRepository.Get();
            return View(news);
        }

        
        // GET: NewsController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News model)
        {
            if (ModelState.IsValid)
            {
                await _NewsRepository.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: NewsController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var newsItem = await _NewsRepository.GetById(id);
            return View(newsItem);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(News model)
        {
            if (ModelState.IsValid)
            {
                await _NewsRepository.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: NewsController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var newsItem = await _NewsRepository.GetById(id);
            return View(newsItem);
        }

        // POST: NewsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _NewsRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
