using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Domain.User.Token.Models;

namespace Auth.Domain.User.Services
{
    public interface IUserService
    {
        Task<AccessToken?> AuthenticateUser(Auth.Domain.User.Models.User user);
    }
}