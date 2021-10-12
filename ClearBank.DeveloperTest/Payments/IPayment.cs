using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Payments
{
    public interface IPayment
    {
        bool PaymentAllowed(Account account, decimal amount);

    }
}