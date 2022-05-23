using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AuthorizationService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<string> Login(UserDto user)
        {
            var hash = new Hashing();
            var userEntityToFind = _mapper.Map<User>(user,
                opts => opts.AfterMap((_,
                    u) => u.Password = hash.GetHashString(u.Password)));
            var userEntity = await _userRepository.GetByLoginPassword(userEntityToFind);

            if (userEntity == null)
                throw new ArgumentException();

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                AuthOptions.Issuer,
                AuthOptions.Audience,
                notBefore: now,
                claims: new List<Claim>
                {
                    new(ClaimsIdentity.DefaultNameClaimType, userEntity.Name),
                    new("surname", userEntity.Surname),
                    new("login", userEntity.Login),
                    new(ClaimsIdentity.DefaultRoleClaimType, userEntity.Role.ToString())
                },
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public bool CheckToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out _);

            return true;
        }

        private static TokenValidationParameters GetValidationParameters()
        {
            return new()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.Issuer,
                ValidAudience = AuthOptions.Audience,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
            };
        }
    }
}