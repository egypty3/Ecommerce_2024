using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITokenGenerationService
    {
        string GenerateToken(IEnumerable<Claim> claims,int expirationMinutes = 60);
    }
}
