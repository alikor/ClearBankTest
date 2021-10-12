using System.Threading.Tasks;

namespace ClearBank.DeveloperTest.Data.Saga.States
{
    internal interface ITransferFundsSagaState
    {
        Task Handle(ITransferFundsSagaContext context, TransferFundsCommand command);

        public string StateName { get; }
    }
}
