using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // private static List<Book> BookList = new List<Book>(){
        //     new Book(){
        //         Id = 1,
        //         Title = "Book1",
        //         GenreId = 1,
        //         PageCount = 500,
        //         PublishDate = new System.DateTime(2005,10,23)
        //     },
        //     new Book(){
        //         Id = 2,
        //         Title = "Book2",
        //         GenreId = 2,
        //         PageCount = 700,
        //         PublishDate = new System.DateTime(2013,11,22)
        //     },
        //     new Book(){
        //         Id = 3,
        //         Title = "Book3",
        //         GenreId = 3,
        //         PageCount = 250,
        //         PublishDate = new System.DateTime(1998,06,17)
        //     }
        // };
        // [HttpGet]
        // public List<Book> GetBooks()
        // {
        //     var bookList = _context.Books.OrderBy(x => x.Id).ToList();
        //     return bookList;
        // }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery querry = new GetBooksQuery(_context, _mapper);
            var result = querry.Handle();
            return Ok(result);
        }

        // [HttpGet("{id}")]
        // public Book GetBookById(int id)
        // {
        //     var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
        //     return book;
        // }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            var book = query.Handle();
            return Ok(book);
        }

        // [HttpPost]
        // public IActionResult AddBook([FromBody] Book newBook)
        // {
        //     var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);
        //     if (book is not null)
        //         return BadRequest();
        //     _context.Books.Add(newBook);
        //     _context.SaveChanges();
        //     return Ok();
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        // [HttpPut("{id}")]
        // public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        // {
        //     var book = _context.Books.SingleOrDefault(x => x.Id == id);
        //     if (book is null)
        //         return BadRequest();

        //     book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
        //     book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
        //     book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
        //     book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
        //     _context.SaveChanges();
        //     return Ok();
        // }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);

            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        // [HttpDelete("{id}")]
        // public IActionResult DeleteBook(int id)
        // {
        //     var book = _context.Books.SingleOrDefault(x => x.Id == id);
        //     if (book is null)
        //     {
        //         return BadRequest();
        //     }
        //     _context.Books.Remove(book);
        //     _context.SaveChanges();
        //     return Ok();
        // }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}