using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Models;
using MyProject.Models.ViewModel;
using System.Linq;

namespace MyProject.Controllers
{
    public class BookController : Controller
    {
        private AppDbContext _db;
        public BookController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var books = _db.Books.ToList();
            return View(books);
        }
        public IActionResult Details(int id)
        {
            var book = _db.Books.Include(b => b.Comments).FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookDetailsViewModel
            {
                Book = book,
                NewCommentText = "" // Инициализируем пустую строку для нового комментария
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int id, string newCommentText)
        {
            var book = _db.Books.Include(b => b.Comments).FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                var comment = new Comment { Text = newCommentText, BookId = id };
                if (book.Comments == null)
                {
                    book.Comments = new List<Comment>(); // Инициализируем список комментариев, если он еще не инициализирован
                }
                book.Comments.Add(comment); // Добавляем комментарий к списку комментариев книги
                _db.Comments.Add(comment); // Добавляем комментарий в контекст данных
                await _db.SaveChangesAsync(); // Сохраняем изменения в базе данных
            }

            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Add(book);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Update(book);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);
        }

    }
}
