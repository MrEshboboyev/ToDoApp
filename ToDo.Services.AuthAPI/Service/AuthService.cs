using Microsoft.AspNetCore.Identity;
using ToDo.Services.AuthAPI.Data;
using ToDo.Services.AuthAPI.Models;
using ToDo.Services.AuthAPI.Models.Dto;
using ToDo.Services.AuthAPI.Service.IService;

namespace ToDo.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if(user != null)
            {
                if(!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    // if role not found, new role create with name : "_roleName"
                    _roleManager.CreateAsync(new IdentityRole { Name = roleName}).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }

            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Username.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if(user == null || !isValid)
            {
                return new LoginResponseDto { User = null, Token = "" };
            }

            // if user was found, Generate JWT token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                PhoneNumber = registrationRequestDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if(result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == user.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }

            return "Error encountered";
        }
    }
}
