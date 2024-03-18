using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Models.ViewModel;
using System.Diagnostics;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<Book> _books;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _books = new List<Book>
            {
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Description = "The story of the mysteriously wealthy Jay Gatsby and his love for the beautiful Daisy Buchanan.", Category = "Novel", Year = 1925 },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Description = "The story of a young"},
                new Book { Id = 3, Title = "1984", Author = "George Orwell", Description = "A dystopian social science fiction novel portraying a totalitarian regime and its effects on society.", Category = "Fiction", Year = 1949 },
                new Book { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen", Description = "A romantic novel exploring themes of love, reputation, and class in early 19th-century England.", Category = "Classic", Year = 1813 },
                new Book { Id = 5, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Description = "A coming-of-age novel following the adventures of Holden Caulfield, a disenchanted teenager in New York City.", Category = "Fiction", Year = 1951 },
                new Book { Id = 6, Title = "Moby-Dick", Author = "Herman Melville", Description = "An epic tale of one man's obsessive quest for revenge against a giant white whale.", Category = "Adventure", Year = 1851 },
                new Book { Id = 7, Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Description = "A high-fantasy epic following the journey of a young hobbit named Frodo Baggins to destroy the One Ring and defeat the Dark Lord Sauron.", Category = "Fantasy", Year = 1954 },
                new Book { Id = 8, Title = "Frankenstein", Author = "Mary Shelley", Description = "A Gothic science fiction novel exploring themes of creation, identity, and morality.", Category = "Horror", Year = 1818 },
                new Book { Id = 9, Title = "Brave New World", Author = "Aldous Huxley", Description = "A dystopian novel set in a future world where genetic engineering and social conditioning control society.", Category = "Science Fiction", Year = 1932 },
                new Book { Id = 10, Title = "The Odyssey", Author = "Homer", Description = "An epic poem recounting the adventures of the Greek hero Odysseus during his journey home from the Trojan War.", Category = "Classic", Year = null },
                new Book { Id = 11, Title = "The Adventures of Huckleberry Finn", Author = "Mark Twain", Description = "A novel following the escapades of a young boy and a runaway slave as they travel down the Mississippi River.", Category = "Adventure", Year = 1884 },
                new Book { Id = 12, Title = "Jane Eyre", Author = "Charlotte Brontë", Description = "A Bildungsroman novel following the life of an orphaned girl as she faces trials and tribulations on her journey to independence and love.", Category = "Romance", Year = 1847 }
            };
        }

        public IActionResult Index(int pg = 1)
        {
            const int pageSize = 5;
            if (pg < 1) pg = 1;
            int recsCount = _books.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = _books.Skip(recSkip).Take(pageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
