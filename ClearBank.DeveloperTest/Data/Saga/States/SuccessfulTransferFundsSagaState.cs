using System.Threading.Tasks;

namespace ClearBank.DeveloperTest.Data.Saga.States
{
    internal class SuccessfulTransferFundsSagaState : ITransferFundsSagaState
    {
        public string StateName => "Successful";

        public Task Handle(ITransferFundsSagaContext context, TransferFundsCommand command)
        {
            throw new SagaException();
        }
    }
}
