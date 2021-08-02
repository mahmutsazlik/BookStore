using System.Linq;
using System.Collections.Generic;
using WebApi.DbOperations;
using WebApi.Models;
using WebApi.Common;
using System;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public CreateBookCommand(BookStoreDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x=>x.Title==Model.Title);
            if(book is not null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut.");
            }
            book = new Book(){
                Title = Model.Title,
                PageCount= Model.PageCount,
                PublishDate=Model.PublishDate,
                GenreId=Model.GenreId
            };
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
    }
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}