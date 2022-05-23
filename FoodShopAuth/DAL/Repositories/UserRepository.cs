using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _db;

        public UserRepository(UserContext db)
        {
            _db = db;
        }

        public async Task<User> Get(Guid id)
        {
            if (await _db.Users.CountAsync(u => u.Id == id) == 0)
                return null;

            return await _db.Users.FirstAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task Create(User item)
        {
            await _db.Users.AddAsync(item);
        }

        public async Task Update(User item)
        {
            if (await _db.Users.CountAsync(u => u.Id == item.Id) == 0)
                return;

            _db.Remove(await _db.Users.FirstAsync(u => u.Id == item.Id));
            await _db.AddAsync(item);
        }

        public async Task Delete(Guid id)
        {
            if (await _db.Users.CountAsync(u => u.Id == id) == 0)
                return;

            _db.Users.Remove(await _db.Users.FirstAsync(u => u.Id == id));
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<User> GetByLoginPassword(User item)
        {

            return await _db.Users.FirstOrDefaultAsync(u =>
                string.Equals(u.Login, item.Login) && string.Equals(u.Password, item.Password));
        }
    }
}