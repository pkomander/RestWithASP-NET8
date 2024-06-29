using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Domain;
using RestWithASPNET.Repository.Data;
using RestWithASPNET.Repository.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET.Repository.Services.Repository
{
    public class BookRepository : IBookService
    {

        private readonly Context _context;
        public BookRepository(Context context)
        {
            _context = context;
        }

        public async Task<Book> Create(Book book)
        {
            try
            {
                _context.Add(book);
                await _context.SaveChangesAsync();

                return book;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Book> FindById(long id)
        {
            try
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

                return book;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Book>> FindAll()
        {
            try
            {
                return await _context.Books.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Book> Update(Book book)
        {
            try
            {
                var model = await _context.Books.FirstOrDefaultAsync(x => x.Id == book.Id);

                if (model == null)
                    return null;

                _context.Entry(model).CurrentValues.SetValues(book);
                await _context.SaveChangesAsync();

                return book;
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
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);

                if (book == null)
                    return false;

                _context.Books.Remove(book);
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
