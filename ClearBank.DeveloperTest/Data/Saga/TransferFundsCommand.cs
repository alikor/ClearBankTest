using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Data.Saga
{
    public class TransferFundsCommand
    {

        public TransferFundsCommand(Account debtorAccount, Account creditorAccount, decimal amount)
        {
            DebtorAccount = debtorAccount;
            CreditorAccount = creditorAccount;
            Amount = amount;
        }

        public Account DebtorAccount { get; }
        public Account CreditorAccount { get; }
        public decimal Amount { get; }
    }
}
