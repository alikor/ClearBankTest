using ClearBank.DeveloperTest.Data.Saga.States;

namespace ClearBank.DeveloperTest.Data.Saga
{
    public class TransferFundsSagaFactory : ITransferFundsSagaFactory
    {
        private readonly IAccountDataStore accountDataStore;

        public TransferFundsSagaFactory(IAccountDataStore accountDataStore)
        {
            this.accountDataStore = accountDataStore;
        }
        public ITransferFundsSaga Create()
        {
            var saga = new TransferFundsSaga();
            ((ITransferFundsSagaContext)saga).SetState(new InitialisedTransferFundsSaga(this.accountDataStore));
            return saga;
        }
    }
}
