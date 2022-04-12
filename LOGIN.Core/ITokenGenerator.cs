using LOGIN.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LOGIN.Core
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(AppUser user);
    }
}