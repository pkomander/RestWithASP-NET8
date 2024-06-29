using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Domain;
using RestWithASPNET.DTO.PersonDto;
using RestWithASPNET.Repository.Data;
using RestWithASPNET.Repository.Services.Interface;

namespace RestWithASPNET.Repository.Services.Repository
{
    public class PersonRepository : IPersonService
    {

        private readonly Context _context;
        private readonly IGenericService _genericService;
        private readonly IMapper _mapper;

        public PersonRepository(Context context, IGenericService genericService, IMapper mapper)
        {
            _context = context;
            _genericService = genericService;
            _mapper = mapper;
        }

        public async Task<ReadPersonDto> Create(CreatePersonDto personDto)
        {
            try
            {
                var person = _mapper.Map<Person>(personDto);
                _genericService.Add<Person>(person);
                await _genericService.SaveChangesAsync();

                return _mapper.Map<ReadPersonDto>(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadPersonDto> FindById(long id)
        {
            try
            {
                var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

                return _mapper.Map<ReadPersonDto>(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ReadPersonDto>> FindAll()
        {
            try
            {
                var persons = await _context.Persons.ToListAsync();

                return _mapper.Map<List<ReadPersonDto>>(persons);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadPersonDto> Update(UpdatePersonDto personDto, int id)
        {
            try
            {
                var model = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

                if (model == null)
                    return null;

                var person = _mapper.Map(personDto, model);

                _genericService.Update<Person>(person);
                await _genericService.SaveChangesAsync();

                return _mapper.Map<ReadPersonDto>(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

                if (person == null)
                    return false;

                _genericService.Delete<Person>(person);
                await _genericService.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
