using ToDo.Services.AuthAPI.Models;
using ToDo.Services.AuthAPI.Service.IService;

namespace ToDo.Services.AuthAPI.Service
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        public JwtTokenGenerator(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public string GenerateToken()
        {
            throw new NotImplementedException();
        }
    }
}
