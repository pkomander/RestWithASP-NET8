using AutoMapper;
using RestWithASPNET.Domain;
using RestWithASPNET.DTO.PersonDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET.Repository.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<CreatePersonDto, Person>();
            CreateMap<Person, ReadPersonDto>();
            CreateMap<UpdatePersonDto, Person>();
        }
    }
}
