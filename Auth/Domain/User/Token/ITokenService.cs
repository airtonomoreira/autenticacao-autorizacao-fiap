using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Domain.User.Token.Models;

namespace Auth.Domain.User.Token
{

    public interface ITokenService
    {
        AccessToken GenerateToken(Auth.Domain.User.Models.User user);
    }
}