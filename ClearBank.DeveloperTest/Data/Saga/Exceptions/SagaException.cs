using System;

namespace ClearBank.DeveloperTest.Data.Saga
{
    public class SagaException : Exception
    {
        public SagaException()
        {
        }

        public SagaException(string message)
            : base("Saga in invalid state")
        {
        }
    }
}
