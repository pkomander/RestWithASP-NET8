using RestWithASPNET.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET.Repository.Services.Interface
{
    public interface IBookService
    {
        Task<Book> Create(Book book);
        Task<Book> FindById(long id);
        Task<List<Book>> FindAll();
        Task<Book> Update(Book book);
        Task<bool> Delete(long id);
    }
}
