using Demografi.Models;

namespace Demografi.Services
{
    public class AccountServiceImpl : IAccountService
    {
        private readonly List<Account> accounts;

        public AccountServiceImpl() 
        {
            accounts = new List<Account>
        {
            new Account
            {
                Id= 1,
                Username="admin@gmail.com",
                Password="admin"
            }
        };
        
        }

        public Account Login(string username, string password)
        {
            return accounts.SingleOrDefault(a => a.Username == username && a.Password == password);
        }
    }
}
