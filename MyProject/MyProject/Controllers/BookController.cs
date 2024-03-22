using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Data;
using MyProject.Models.ViewModel;

namespace MyProject.Controllers
{
    public class BookController : Controller
    {
        private AppDbContext _db;
        public BookController(AppDbContext db)
        {
            _db = db;
        }
        [Authorize(Policy = "AgeLimit")] // user must atleast be 18 years old
        public IActionResult Index(int pg = 1)
        {
            const int pageSize = 5;
            if (pg < 1) pg = 1;

            var allBooks = _db.Books.ToList();

            int recsCount = allBooks.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = allBooks.Skip(recSkip).Take(pageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
        }
    }
}
