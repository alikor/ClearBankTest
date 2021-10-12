using ClearBank.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Payments
{
    public class FasterPayments : IPayment
    {
        public bool PaymentAllowed(Account account, decimal amount)
        {

            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
            {
                return false;
            }
            else if (account.Balance < amount)
            {
                return false;
            }
            return true;

        }
    }
}
