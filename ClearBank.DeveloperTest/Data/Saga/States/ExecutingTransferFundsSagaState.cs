using System.Threading.Tasks;

namespace ClearBank.DeveloperTest.Data.Saga.States
{
    internal class ExecutingTransferFundsSagaState : ITransferFundsSagaState
    {
        public string StateName => "Executing";

        public Task Handle(ITransferFundsSagaContext context, TransferFundsCommand command)
        {
            throw new SagaException();
        }
    }
}
