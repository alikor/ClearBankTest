using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Payments
{
    public class PaymentFactory : IPaymentFactory
    {
        public IPayment GetPayment(PaymentScheme paymentScheme)
        {
            switch (paymentScheme)
            {
                case PaymentScheme.Bacs:
                    return new BacsPayment();

                case PaymentScheme.FasterPayments:
                    return new FasterPayments();

                default:
                    return new ChapsPayment();
            }
        }
    }
}