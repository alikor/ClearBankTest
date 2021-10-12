using ClearBank.DeveloperTest.Data.Saga.States;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Data.Saga
{
    public class TransferFundsSaga : ITransferFundsSagaContext, ITransferFundsSaga
    {
        private ITransferFundsSagaState state;

        public string StateName => state.StateName;

        void ITransferFundsSagaContext.SetState(ITransferFundsSagaState state)
        {
            this.state = state;
        }

        public void Handle(TransferFundsCommand command) => state.Handle(this, command).Wait();


    }
}
