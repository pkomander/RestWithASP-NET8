using AutoMapper;
using RestWithASPNET.Domain.Identity;
using RestWithASPNET.DTO.UserDto;
using RestWithASPNET.Repository.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RestWithASPNET.Repository.Services.Repository
{
    public class AccountRepository : IAccountService
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public AccountRepository(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IMapper mapper,
                              IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var user = await _userManager.Users
                                             .SingleOrDefaultAsync(user => user.UserName == userUpdateDto.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _mapper.Map<UserUpdateDto>(user);
                    return userToReturn;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar Criar Usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUserNameAsync(string username)
        {
            try
            {
                var user = await _userService.GetUserByUsernameAsync(username);
                if (user == null) return null;

                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
                return userUpdateDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar pegar Usuário por Username. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userService.GetUserByUsernameAsync(userUpdateDto.UserName);
                if (user == null) return null;

                userUpdateDto.Id = user.Id;

                _mapper.Map(userUpdateDto, user);

                if (userUpdateDto.Password != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);
                }

                _userService.Update<User>(user);

                if (await _userService.SaveChangesAsync())
                {
                    var userRetorno = await _userService.GetUserByUsernameAsync(user.UserName);

                    return _mapper.Map<UserUpdateDto>(userRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UserExists(string username)
        {
            try
            {
                return await _userManager.Users.AnyAsync(user => user.UserName == username.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao verificar se usuário existe. Erro: {ex.Message}");
            }
        }
    }
}
