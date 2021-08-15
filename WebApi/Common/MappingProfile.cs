using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.GenreOperations.Commands;
using WebApi.Application.GenreOperations.Queries;
using WebApi.Models;
using WebApi.Application.BookOperations.Queries.GetBooksDetail;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookViewModel>()
                                            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                                            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")))
                                            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName));
            CreateMap<Book, BookDetailViewModel>()
                                            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                                            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy")))
                                            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName));
            CreateMap<Genre, GenreViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<Author, AuthorViewModel>()
                                            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.Date.ToString("dd/MM/yyyy")));
            CreateMap<Author, AuthorDetailViewModel>()
                                            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.Date.ToString("dd/MM/yyyy")));
        }
    }
}