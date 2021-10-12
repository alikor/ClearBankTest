using ClearBank.DeveloperTest.Types;
using System;
using System.Threading.Tasks;

namespace ClearBank.DeveloperTest.Data.Saga.States
{
    internal class InitialisedTransferFundsSaga : ITransferFundsSagaState
    {
        private readonly IAccountDataStore accountDataStore;

        public string StateName => "Initialised";


        public InitialisedTransferFundsSaga(IAccountDataStore accountDataStore)
        {
            this.accountDataStore = accountDataStore;
        }

        public async Task Handle(ITransferFundsSagaContext context, TransferFundsCommand command)
        {
            context.SetState(new ExecutingTransferFundsSagaState());

            var debtorBalance = command.DebtorAccount.Balance;
            var creditorBalance = command.CreditorAccount.Balance;

            command.DebtorAccount.Balance -= command.Amount;
            command.CreditorAccount.Balance += command.Amount;

            var debtorTask = Task.Run(() => accountDataStore.UpdateAccount(command.DebtorAccount));
            var creditorTask = Task.Run(() => accountDataStore.UpdateAccount(command.CreditorAccount));

            try
            {
                await Task.WhenAll(debtorTask, creditorTask);
                context.SetState(new SuccessfulTransferFundsSagaState());

            }
            catch (Exception)
            {
                if (debtorTask.IsFaulted == false)
                {
                    await RevertBalance(command.DebtorAccount, debtorBalance);
                }

                if (creditorTask.IsFaulted == false)
                {
                    await RevertBalance(command.CreditorAccount, creditorBalance);
                }

                context.SetState(new FailedTransferFundsSagaState());
            }
        }

        private async Task RevertBalance(Account account, decimal balance)
        {
            account.Balance = balance;
            await Task.Run(() => accountDataStore.UpdateAccount(account));

        }
    }
}
