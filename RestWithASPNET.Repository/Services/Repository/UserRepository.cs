using Microsoft.EntityFrameworkCore;
using RestWithASPNET.Domain.Identity;
using RestWithASPNET.Repository.Data;
using RestWithASPNET.Repository.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET.Repository.Services.Repository
{
    public class UserRepository : GenericRepository, IUserService
    {

        private readonly Context _context;

        public UserRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                return await _context.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
