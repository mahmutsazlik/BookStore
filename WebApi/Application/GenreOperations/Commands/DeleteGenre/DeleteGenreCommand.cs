using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Silinecek kitap türü bulunamadı.");
            }
            genre.IsActive = false;
            //_context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }

}