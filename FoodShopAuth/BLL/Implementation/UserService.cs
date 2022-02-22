using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Implementation
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User> Get(Guid id)
        {
            if (await _userRepository.Get(id) == null)
                throw new ArgumentException();

            return await _userRepository.Get(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task Create(UserDto item)
        {
            var hash = new Hashing();
            var user = _mapper.Map<User>(item,
                opts => opts.AfterMap((_,
                    u) => u.Password = hash.GetHashString(u.Password)));

            await _userRepository.Create(user);

            await _userRepository.Save();
        }

        public async Task Update(Guid id, UserDto item)
        {
            if (await _userRepository.Get(id) == null)
                throw new ArgumentException();

            var hash = new Hashing();

            var user = _mapper.Map<User>(item,
                opts => opts.AfterMap((_,
                    u) =>
                {
                    u.Id = id;
                    u.Password = hash.GetHashString(u.Password);
                }));
            await _userRepository.Update(user);

            await _userRepository.Save();
        }

        public async Task Delete(Guid id)
        {
            if (await _userRepository.Get(id) == null)
                throw new ArgumentException();

            await _userRepository.Delete(id);
            await _userRepository.Save();
        }
    }
}