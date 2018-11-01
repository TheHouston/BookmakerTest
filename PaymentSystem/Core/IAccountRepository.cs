namespace PaymentSystem.Core
{
    public interface IAccountRepository
    {
        bool CheckFunds(int userId, decimal betSum);
    }
}
