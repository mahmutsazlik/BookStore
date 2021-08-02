using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.Models;

namespace WebApi.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel,Book>();
        }
    }
}