using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Data.Saga;
using ClearBank.DeveloperTest.Payments;
using ClearBank.DeveloperTest.Types;
using System.Configuration;
using System.Transactions;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore accountDataStore;
        private readonly IPaymentFactory paymentFactory;
        private readonly ITransferFundsSagaFactory transferFundsSagaFactory;

        public PaymentService(IAccountDataStore accountDataStore, IPaymentFactory paymentFactory, ITransferFundsSagaFactory transferFundsSagaFactory)
        {
            this.accountDataStore = accountDataStore;
            this.paymentFactory = paymentFactory;
            this.transferFundsSagaFactory = transferFundsSagaFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var debtorAccount = this.accountDataStore.GetAccount(request.DebtorAccountNumber);
            var creditorAccount = this.accountDataStore.GetAccount(request.CreditorAccountNumber);

            if (IsPaymentAllowed(request, debtorAccount))
            {
                return new MakePaymentResult { 
                    Success = TransferFunds(debtorAccount, creditorAccount, request.Amount)
                };
            }

            return new MakePaymentResult { Success = false };
        }

        private bool TransferFunds(Account debtorAccount, Account creditorAccount, decimal amount)
        {
            var saga = this.transferFundsSagaFactory.Create();

            saga.Handle(new TransferFundsCommand(creditorAccount, debtorAccount, amount));

            return saga.StateName == "Successful";
        }

        private bool IsPaymentAllowed(MakePaymentRequest request, Account account)
        {
            return this.paymentFactory
                .GetPayment(request.PaymentScheme)
                .PaymentAllowed(account, request.Amount);
        }
    }
}
