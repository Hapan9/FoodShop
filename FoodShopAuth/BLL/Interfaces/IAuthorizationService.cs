using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IAuthorizationService
    {
        Task<string> Login(UserDto user);

        bool CheckToken(string token);
    }
}
