using System.Threading.Tasks;
using Auth.Domain.User.Token.Models;
using Auth.Domain.User.Repository;
using Auth.Domain.User.Token;

namespace Auth.Domain.User.Services
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<AccessToken?> AuthenticateUser(Auth.Domain.User.Models.User userData)
        {
            if (string.IsNullOrEmpty(userData.Username) || string.IsNullOrEmpty(userData.Password))
            {
                return null;
            }

            var user = await _userRepository.Get(userData.Username, userData.Password);

            if (user is null)
            {
                return null;
            }

            return _tokenService.GenerateToken(user);
        }
    }
}