using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Data.Saga;
using ClearBank.DeveloperTest.Types;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Data.Saga
{
    public class TransferFundsSagaTests
    {
        public class HandleMethod
        {
            [Fact]
            public void When_Debtor_and_Creditor_are_successful_StateName_Successful_and_balances_updated()
            {
                var debtorAccountNumber = "1234";
                var creditorAccountNumber = "4321";
                var amount = 30M;
                var accountDataStoreMock = new Mock<IAccountDataStore>();
                var factory = new TransferFundsSagaFactory(accountDataStoreMock.Object);

                var subject = factory.Create();

                var command = new TransferFundsCommand(
                    new Account
                    {
                        AccountNumber = debtorAccountNumber,
                        Balance = 40M
                    },
                    new Account
                    {
                        AccountNumber = creditorAccountNumber
                    },
                    amount);

                subject.Handle(command);

                accountDataStoreMock.Verify(x => x.UpdateAccount(It.Is<Account>(a => a.AccountNumber == debtorAccountNumber && a.Balance == 10M)), Times.Once);
                accountDataStoreMock.Verify(x => x.UpdateAccount(It.Is<Account>(a => a.AccountNumber == creditorAccountNumber && a.Balance == 30M)), Times.Once);
                Assert.Equal("Successful", subject.StateName);

            }

            [Fact]
            public void When_Debtor_successful_and_Creditor_failed_StateName_failed_and_debtor_balance_reimburse()
            {
                var debtorAccountNumber = "1234";
                var creditorAccountNumber = "4321";
                var amount = 30M;
                var accountDataStoreMock = new Mock<IAccountDataStore>();
                var factory = new TransferFundsSagaFactory(accountDataStoreMock.Object);
                accountDataStoreMock.Setup(x => x.UpdateAccount(It.Is<Account>(a => a.AccountNumber == creditorAccountNumber))).Throws(new Exception()).Verifiable();
                accountDataStoreMock.Setup(x => x.UpdateAccount(It.Is<Account>(a => a.AccountNumber == debtorAccountNumber && a.Balance == 10M))).Verifiable();
                accountDataStoreMock.Setup(x => x.UpdateAccount(It.Is<Account>(a => a.AccountNumber == debtorAccountNumber && a.Balance == 40M))).Verifiable();


                var subject = factory.Create();

                var command = new TransferFundsCommand(
                    new Account
                    {
                        AccountNumber = debtorAccountNumber,
                        Balance = 40M
                    },
                    new Account
                    {
                        AccountNumber = creditorAccountNumber
                    },
                    amount);

                subject.Handle(command);

                accountDataStoreMock.Verify();
                Assert.Equal("Failed", subject.StateName);

            }

            [Fact]
            public void When_Creditor_successful_and_Debtor_failed_StateName_failed_and_Creditor_balance_reimburse()
            {
                var debtorAccountNumber = "1234";
                var creditorAccountNumber = "4321";
                var amount = 30M;
                var accountDataStoreMock = new Mock<IAccountDataStore>();
                var factory = new TransferFundsSagaFactory(accountDataStoreMock.Object);
                accountDataStoreMock.Setup(x => x.UpdateAccount(It.Is<Account>(a => a.AccountNumber == debtorAccountNumber))).Throws(new Exception()).Verifiable();
                accountDataStoreMock.Setup(x => x.UpdateAccount(It.Is<Account>(a => a.AccountNumber == creditorAccountNumber && a.Balance == 90M))).Verifiable();
                accountDataStoreMock.Setup(x => x.UpdateAccount(It.Is<Account>(a => a.AccountNumber == creditorAccountNumber && a.Balance == 60M))).Verifiable();


                var subject = factory.Create();

                var command = new TransferFundsCommand(
                    new Account
                    {
                        AccountNumber = debtorAccountNumber,
                        Balance = 40M
                    },
                    new Account
                    {
                        AccountNumber = creditorAccountNumber,
                        Balance = 60M
                    },
                    amount);

                subject.Handle(command);

                accountDataStoreMock.Verify();
                Assert.Equal("Failed", subject.StateName);

            }
        }
    }
}
