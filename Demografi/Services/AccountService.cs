using Demografi.Models;

namespace Demografi.Services
{
    public interface IAccountService
    {
        public Account Login(string username, string password);
    }
}
