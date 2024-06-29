using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Data;
using RestWithASPNET.Model;
using RestWithASPNET.Services.Interface;

namespace RestWithASPNET.Services.Repository
{
    public class PersonRepository : IPersonService
    {

        private readonly Context _context;

        public PersonRepository(Context context)
        {
            _context = context;
        }

        public async Task<Person> Create(Person person)
        {
            try
            {
                _context.Add(person);
                await _context.SaveChangesAsync();

                return person;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Person> FindById(long id)
        {
            try
            {
                var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);

                return person;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Person>> FindAll()
        {
            try
            {
                var persons = await _context.Persons.ToListAsync();

                return persons;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Person> Update(Person person)
        {
            try
            {
                var model = await _context.Persons.FirstOrDefaultAsync(x => x.Id == person.Id);

                if (model == null)
                    return null;

                _context.Entry(model).CurrentValues.SetValues(person);
                await _context.SaveChangesAsync();

                return person;
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

                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
