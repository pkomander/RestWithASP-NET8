using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RestWithASPNET.Domain.Identity;
using RestWithASPNET.DTO.UserDto;
using RestWithASPNET.Repository.Services.Interface;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestWithASPNET.Repository.Services.Repository
{
    public class TokenRepository : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public readonly SymmetricSecurityKey _key;

        public TokenRepository(IConfiguration config,
                                UserManager<User> userManager,
                                IMapper mapper)
        {
            _config = config;
            _userManager = userManager;
            _mapper = mapper;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public async Task<string> CreateToken(UserUpdateDto userUpdateDto)
        {
            var user = _mapper.Map<User>(userUpdateDto);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenKey = "super-secret-key";

            var keyBytes = Encoding.UTF8.GetBytes(tokenKey);
            if (keyBytes.Length < 64) // 64 bytes = 512 bits
            {
                var paddedKeyBytes = new byte[64]; // Tamanho mínimo da chave para HMAC-SHA512
                Array.Copy(keyBytes, paddedKeyBytes, keyBytes.Length);
                keyBytes = paddedKeyBytes;
            }

            var key = new SymmetricSecurityKey(keyBytes);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
