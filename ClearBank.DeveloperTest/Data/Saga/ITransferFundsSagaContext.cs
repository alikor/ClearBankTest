using ClearBank.DeveloperTest.Data.Saga.States;

namespace ClearBank.DeveloperTest.Data.Saga
{
    internal interface ITransferFundsSagaContext
    {
        void SetState(ITransferFundsSagaState state);
    }
}
