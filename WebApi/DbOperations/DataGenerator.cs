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
                if (context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
                    new Book()
                    {
                        // Id = 1,
                        Title = "Book1",
                        GenreId = 1,
                        PageCount = 500,
                        PublishDate = new System.DateTime(2005, 10, 23)
                    },
                    new Book()
                    {
                        // Id = 2,
                        Title = "Book2",
                        GenreId = 2,
                        PageCount = 700,
                        PublishDate = new System.DateTime(2013, 11, 22)
                    },
                    new Book()
                    {
                        // Id = 3,
                        Title = "Book3",
                        GenreId = 3,
                        PageCount = 250,
                        PublishDate = new System.DateTime(1998, 06, 17)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}