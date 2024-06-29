using RestWithASPNET.Model;
using RestWithASPNET.Services.Interface;

namespace RestWithASPNET.Services.Repository
{
    public class PersonRepository : IPersonService
    {

        //public PersonRepository()
        //{

        //}
        public Task<Person> Create(Person person)
        {
            throw new NotImplementedException();
        }

        public Task<Person> FindById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Person>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<Person> Update(Person person)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
