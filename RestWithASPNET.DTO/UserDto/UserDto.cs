using RestWithASPNET.Domain.Enum;
using RestWithASPNET.Domain.Identity;

namespace RestWithASPNET.DTO.UserDto
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
    }
}
