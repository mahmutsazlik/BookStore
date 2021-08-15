using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Models;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any() && context.Genres.Any() && context.Authors.Any())
                {
                    return;
                }
                context.Books.AddRange(
                    new Book()
                    {
                        Title = "Book1",
                        GenreId = 1,
                        PageCount = 500,
                        PublishDate = new System.DateTime(2005, 10, 23),
                        AuthorId=1
                    },
                    new Book()
                    {
                        Title = "Book2",
                        GenreId = 2,
                        PageCount = 700,
                        PublishDate = new System.DateTime(2013, 11, 22),
                        AuthorId=2
                    },
                    new Book()
                    {
                        Title = "Book3",
                        GenreId = 3,
                        PageCount = 250,
                        PublishDate = new System.DateTime(1998, 06, 17),
                        AuthorId=3
                    }
                );
                context.Genres.AddRange(
                    new Genre()
                    {
                        Name = "Personal Growth"
                    },
                    new Genre()
                    {
                        Name = "Science Fiction"
                    },
                    new Genre()
                    {
                        Name = "Noval"
                    }
                );
                context.Authors.AddRange(
                    new Author(){
                        FirstName="AuthorFN1",
                        LastName="AuthorLN1",
                        Birthday=new DateTime(1990,10,05)
                    },
                    new Author(){
                        FirstName="AuthorFN2",
                        LastName="AuthorLN2",
                        Birthday=new DateTime(2000,06,18)
                    },
                    new Author(){
                        FirstName="AuthorFN3",
                        LastName="AuthorLN3",
                        Birthday=new DateTime(1985,04,23)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}