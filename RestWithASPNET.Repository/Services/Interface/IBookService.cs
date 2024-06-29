using RestWithASPNET.Domain;
using RestWithASPNET.DTO.BookDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET.Repository.Services.Interface
{
    public interface IBookService
    {
        Task<ReadBookDto> Create(CreateBookDto bookDto);
        Task<ReadBookDto> FindById(long id);
        Task<List<ReadBookDto>> FindAll();
        Task<ReadBookDto> Update(UpdateBookDto book, int id);
        Task<bool> Delete(long id);
    }
}
