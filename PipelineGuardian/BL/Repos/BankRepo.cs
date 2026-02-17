using Data.Models;

namespace BL.Repos
{
    public class BankRepository
    {
        private List<BankAccount> _accounts = new List<BankAccount>
        {
            new BankAccount { Id = 1, Owner = "Ahmed", Balance = 5000, IsFrozen = false },
            new BankAccount { Id = 3, Owner = "Hacker", Balance = 0, IsFrozen = true }
        };

        public BankAccount GetAccount(int id) => _accounts.FirstOrDefault(a => a.Id == id);
    }
}