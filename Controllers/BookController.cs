using LibraryManagement.Data.Repositories.Abstract;
using LibraryManagement.Entities;
using LibraryManagement.Models.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            var viewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Publisher = book.Publisher,
                PageCount = book.PageCount,
                ImageUrl = book.ImageUrl
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateViewModel viewModel, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                if (imageFile != null)
                {
                    var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(extension))
                    {
                        ModelState.AddModelError("ImageUrl", "Geçerli bir resim formatı giriniz.!");
                    }
                    else
                    {
                        var randomFileName = $"{Guid.NewGuid()}{extension}"; // rastgele fileName oluşturuluyor

                        var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                        if (!Directory.Exists(uploadDirectory))
                        {
                            Directory.CreateDirectory(uploadDirectory);
                        }
                        var uploadPath = Path.Combine(uploadDirectory, randomFileName);


                        using (var stream = new FileStream(uploadPath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);   // Resmi kaydediyoruz
                        }

                        viewModel.ImageUrl = $"/images/{randomFileName}";
                    }
                }

                var book = new Book
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Publisher = viewModel.Publisher,
                    PageCount = viewModel.PageCount,
                    ImageUrl = viewModel.ImageUrl,
                    CreatedDate = DateTime.Now
                };

                await _bookRepository.AddAsync(book);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> List()
        {
            var books = await _bookRepository.GetAllAsync();
            var viewModels = books.Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Publisher = b.Publisher,
                PageCount = b.PageCount,
                ImageUrl = b.ImageUrl,
                CreatedDate = b.CreatedDate,
                UpdateDate = b.UpdateDate
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return NotFound();

            var viewModel = new BookUpdateViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Publisher = book.Publisher,
                PageCount = book.PageCount,
                ImageUrl = book.ImageUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BookUpdateViewModel viewModel, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                var book = await _bookRepository.GetByIdAsync(viewModel.Id);
                if (book == null)
                    return NotFound();

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                if (imageFile != null)
                {
                    var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(extension))
                        ModelState.AddModelError("ImageUrl", "Geçerli bir resim formatı giriniz.!");
                    else
                    {
                        var randomFileName = $"{Guid.NewGuid()}{extension}"; // rastgele fileName oluşturur

                        var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                        if (!Directory.Exists(uploadDirectory))
                        {
                            Directory.CreateDirectory(uploadDirectory);
                        }

                        var uploadPath = Path.Combine(uploadDirectory, randomFileName);

                        using (var stream = new FileStream(uploadPath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);   // Resmi kaydediyoruz
                        }

                        viewModel.ImageUrl = $"/images/{randomFileName}";
                    }
                }

                book.Title = viewModel.Title;
                book.Description = viewModel.Description;
                book.Publisher = viewModel.Publisher;
                book.PageCount = viewModel.PageCount;
                book.ImageUrl = viewModel.ImageUrl;
                book.UpdateDate = DateTime.UtcNow;

                await _bookRepository.UpdateAsync(book);
                return RedirectToAction("List");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book != null)
            {
                await _bookRepository.DeleteAsync(book);
            }
            return RedirectToAction("List");
        }
    }
}
