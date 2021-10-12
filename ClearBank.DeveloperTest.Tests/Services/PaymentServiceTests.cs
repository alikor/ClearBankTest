using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Data.Saga;
using ClearBank.DeveloperTest.Payments;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace ClearBank.DeveloperTest.Tests.Services
{
    public class PaymentServiceTests
    {
        public class MakePaymentMethod
        {
            [Fact]
            public void When_IsPaymentAllowed_true_And_transferSaga_Successful_returns_success()
            {
                var debtorAccountNumber = "1234";
                var creditorAccountNumber = "4321";
                var accountDataStoreMock = new Mock<IAccountDataStore>();
                var paymentFactoryMock = new Mock<IPaymentFactory>();
                var paymentyMock = new Mock<IPayment>();
                var transferFundsSagaFactoryMock = new Mock<ITransferFundsSagaFactory>();
                var transferFundsSagaMock = new Mock<ITransferFundsSaga>();

                accountDataStoreMock.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(new Account() { AccountNumber = debtorAccountNumber, Balance = 30 });
                accountDataStoreMock.Setup(x => x.GetAccount(creditorAccountNumber)).Returns(new Account() { AccountNumber = creditorAccountNumber });
                paymentFactoryMock.Setup(x => x.GetPayment(It.IsAny<PaymentScheme>())).Returns(paymentyMock.Object);
                paymentyMock.Setup(x => x.PaymentAllowed(It.IsAny<Account>(), It.IsAny<decimal>())).Returns(true);
                transferFundsSagaMock.Setup(x => x.StateName).Returns("Successful");
                transferFundsSagaFactoryMock.Setup(x => x.Create()).Returns(transferFundsSagaMock.Object);


                IPaymentService paymentService = new PaymentService(accountDataStoreMock.Object, paymentFactoryMock.Object, transferFundsSagaFactoryMock.Object);

                var result = paymentService.MakePayment(new MakePaymentRequest
                {
                    DebtorAccountNumber = debtorAccountNumber,
                    CreditorAccountNumber = creditorAccountNumber,
                    Amount = 10
                });

                Assert.True(result.Success);
            }


            [Fact]
            public void When_IsPaymentAllowed_true_And_transferSaga_Failed_returns_success_is_false()
            {
                var debtorAccountNumber = "1234";
                var creditorAccountNumber = "4321";
                var accountDataStoreMock = new Mock<IAccountDataStore>();
                var paymentFactoryMock = new Mock<IPaymentFactory>();
                var paymentyMock = new Mock<IPayment>();
                var transferFundsSagaFactoryMock = new Mock<ITransferFundsSagaFactory>();
                var transferFundsSagaMock = new Mock<ITransferFundsSaga>();

                accountDataStoreMock.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(new Account() { AccountNumber = debtorAccountNumber, Balance = 30 });
                accountDataStoreMock.Setup(x => x.GetAccount(creditorAccountNumber)).Returns(new Account() { AccountNumber = creditorAccountNumber });
                paymentFactoryMock.Setup(x => x.GetPayment(It.IsAny<PaymentScheme>())).Returns(paymentyMock.Object);
                paymentyMock.Setup(x => x.PaymentAllowed(It.IsAny<Account>(), It.IsAny<decimal>())).Returns(true);
                transferFundsSagaMock.Setup(x => x.StateName).Returns("Failed");
                transferFundsSagaFactoryMock.Setup(x => x.Create()).Returns(transferFundsSagaMock.Object);


                IPaymentService paymentService = new PaymentService(accountDataStoreMock.Object, paymentFactoryMock.Object, transferFundsSagaFactoryMock.Object);

                var result = paymentService.MakePayment(new MakePaymentRequest
                {
                    DebtorAccountNumber = debtorAccountNumber,
                    CreditorAccountNumber = creditorAccountNumber,
                    Amount = 10
                });

                Assert.False(result.Success);
            }

            [Fact]
            public void When_IsPaymentAllowed_false_returns_success_is_false()
            {
                var debtorAccountNumber = "1234";
                var accountDataStoreMock = new Mock<IAccountDataStore>();
                var paymentFactoryMock = new Mock<IPaymentFactory>();
                var paymentyMock = new Mock<IPayment>();

                accountDataStoreMock.Setup(x => x.GetAccount(debtorAccountNumber)).Returns(new Account() { AccountNumber = debtorAccountNumber, Balance = 30 });
                paymentFactoryMock.Setup(x => x.GetPayment(It.IsAny<PaymentScheme>())).Returns(paymentyMock.Object);
                paymentyMock.Setup(x => x.PaymentAllowed(It.IsAny<Account>(), It.IsAny<decimal>())).Returns(false);


                IPaymentService paymentService = new PaymentService(accountDataStoreMock.Object, paymentFactoryMock.Object, null);

                var result = paymentService.MakePayment(new MakePaymentRequest
                {
                    DebtorAccountNumber = debtorAccountNumber,
                    Amount = 10
                });

                Assert.False(result.Success);
            }
        }
    }
}
