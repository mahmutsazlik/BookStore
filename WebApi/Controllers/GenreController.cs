using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Queries;
using WebApi.DbOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController:ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context,_mapper);
            var lst = query.Handle();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context,_mapper);
            query.GenreId = id;
            var obj = query.Handle();
            return Ok(obj);
        }
    }
}