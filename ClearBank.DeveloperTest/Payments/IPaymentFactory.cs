using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Payments
{
    public interface IPaymentFactory
    {
        IPayment GetPayment(PaymentScheme paymentScheme);
    }
}