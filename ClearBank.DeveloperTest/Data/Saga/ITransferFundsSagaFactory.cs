namespace ClearBank.DeveloperTest.Data.Saga
{
    public interface ITransferFundsSagaFactory
    {
        ITransferFundsSaga Create();
    }
}