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
    public class GenericRepository : IGenericService
    {
        private readonly Context _context;
        public GenericRepository(Context context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.AddAsync(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
