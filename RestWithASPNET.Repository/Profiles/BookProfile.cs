using AutoMapper;
using RestWithASPNET.Domain;
using RestWithASPNET.DTO.BookDto;
using RestWithASPNET.DTO.PersonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET.Repository.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookDto, Book>();
            CreateMap<Book, ReadBookDto>();
            CreateMap<UpdateBookDto, Book>();
        }
    }
}
