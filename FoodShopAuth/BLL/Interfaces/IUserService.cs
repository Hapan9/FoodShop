using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Dto;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<User> Get(Guid id);
        Task<IEnumerable<User>> GetAll();
        Task Create(UserDto item);
        Task Update(Guid id, UserDto item);
        Task Delete(Guid id);
    }
}