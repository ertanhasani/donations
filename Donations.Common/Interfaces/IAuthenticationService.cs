using Donations.Common.DTOs;
using System.Threading.Tasks;

namespace Donations.Common.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Login(Credentials credentials);
        Task Logout();
    }
}
