using AcmeCorp.Repository.interfaces;
using AcmeCorp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AcmeCorp.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsRepository _NewsRepository;
        private readonly IBlogsRepository _BlogsRepository;


        public HomeController(INewsRepository newRepository, IBlogsRepository blogsRepository)
        {
            _NewsRepository = newRepository;
            _BlogsRepository = blogsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TempData["flash"] = "Thank you, we will reply soon.";
            return View();
        }

        public IActionResult Team()
        {
            return View();
        }

        public async Task<IActionResult> Blogs()
        {
            var news = await _BlogsRepository.Get(true);
            return View(news);
        }
        public async Task<IActionResult> Blog(Guid id)
        {
            var blog = await _BlogsRepository.GetById(id);
            return View(blog);
        }
        public async Task<IActionResult> News()
        {
            var news = await _NewsRepository.Get();
            return View(news);
        }


        public IActionResult ReadMe()
        {
            return View();
        }

    }
}
