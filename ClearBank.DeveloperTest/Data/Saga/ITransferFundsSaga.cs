namespace ClearBank.DeveloperTest.Data.Saga
{
    public interface ITransferFundsSaga
    {
        void Handle(TransferFundsCommand command);

        string StateName { get; }
    }
}