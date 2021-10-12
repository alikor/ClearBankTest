using ClearBank.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Payments
{
    public class ChapsPayment : IPayment
    {
        public bool PaymentAllowed(Account account, decimal amount)
        {
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            {
                return false;
            }
            else if (account.Status != AccountStatus.Live)
            {
                return false;
            }
            return true;
        }
    }
}
