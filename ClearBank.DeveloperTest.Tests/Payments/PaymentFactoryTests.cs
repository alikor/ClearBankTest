using ClearBank.DeveloperTest.Payments;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Payments
{
    public class PaymentFactoryTests
    {
        public class GetPaymentMethod
        {
            [Fact]
            public void When_PaymentScheme_Of_Bacs_return_BacsPayment_instance()
            {
                IPaymentFactory factory = new PaymentFactory();

               var result = factory.GetPayment(Types.PaymentScheme.Bacs);

                Assert.NotNull(result);
                Assert.IsType<BacsPayment>(result);
            }

            [Fact]
            public void When_PaymentScheme_Of_FasterPayments_return_FasterPayments_instance()
            {
                IPaymentFactory factory = new PaymentFactory();

                var result = factory.GetPayment(Types.PaymentScheme.FasterPayments);

                Assert.NotNull(result);
                Assert.IsType<FasterPayments>(result);
            }

            [Fact]
            public void When_PaymentScheme_Of_Chaps_return_ChapsPayment_instance()
            {
                IPaymentFactory factory = new PaymentFactory();

                var result = factory.GetPayment(Types.PaymentScheme.Chaps);

                Assert.NotNull(result);
                Assert.IsType<ChapsPayment>(result);
            }
        }
    }
}
