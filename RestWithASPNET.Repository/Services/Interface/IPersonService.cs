using RestWithASPNET.Domain;
using RestWithASPNET.DTO.PersonDto;

namespace RestWithASPNET.Repository.Services.Interface
{
    public interface IPersonService
    {
        Task<ReadPersonDto> Create(CreatePersonDto personDto);
        Task<ReadPersonDto> FindById(long id);
        Task<List<ReadPersonDto>> FindAll();
        Task<ReadPersonDto> Update(UpdatePersonDto personDto, int id);
        Task<bool> Delete(long id);
    }
}
