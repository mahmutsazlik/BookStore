using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.DbOperations;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            this._context=context;
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
            GetBooksQuerry querry=new GetBooksQuerry(_context);
            var result = querry.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Book GetBookById(int id)
        {
            var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
            return book;
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
            CreateBookCommand command=new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}