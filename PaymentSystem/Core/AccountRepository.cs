using Microsoft.AspNetCore.Mvc;
using PaymentSystem.PaymentModel;
using System;
using System.Linq;

namespace PaymentSystem.Core
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _context;
        public AccountRepository(AccountContext context)
        {
            _context = context;
        }

        [HttpPost("CheckFunds")]
        public bool CheckFunds(int userId, decimal betSum)
        {
            var user = _context.UserAccounts.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
                throw new Exception("User account does not exists");
            return user.Amount >= betSum;
        }
    }
}
