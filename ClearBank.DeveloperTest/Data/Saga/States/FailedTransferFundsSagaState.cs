using System.Threading.Tasks;

namespace ClearBank.DeveloperTest.Data.Saga.States
{
    internal class FailedTransferFundsSagaState : ITransferFundsSagaState
    {
        public string StateName => "Failed";

        public Task Handle(ITransferFundsSagaContext context, TransferFundsCommand command)
        {
            throw new SagaException();
        }
    }
}
