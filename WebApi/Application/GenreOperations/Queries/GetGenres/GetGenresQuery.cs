using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Queries
{
    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<GenreViewModel> Handle()
        {
            var genreList = _context.Genres.OrderBy(x=>x.Id).ToList();
            List<GenreViewModel> lst = _mapper.Map<List<GenreViewModel>>(genreList);
            return lst;
        }
    }

    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}