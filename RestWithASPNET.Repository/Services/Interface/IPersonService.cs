using RestWithASPNET.Domain;

namespace RestWithASPNET.Repository.Services.Interface
{
    public interface IPersonService
    {
        Task<Person> Create(Person person);
        Task<Person> FindById(long id);
        Task<List<Person>> FindAll();
        Task<Person> Update(Person person);
        Task<bool> Delete(long id);
    }
}
