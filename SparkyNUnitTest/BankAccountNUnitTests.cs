using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class BankAccountNUnitTests
    {
        private BankAccount account;
        [SetUp]
        public void Setup()
        {
        }

        //[Test]
        //public void BankAccountLogFakker_Add100_ReturnTrue()
        //{
        //    BankAccount bankAccount = new BankAccount(new LogFakker());
        //    var result = bankAccount.Deposit(100);
        //    Assert.That(result, Is.True);
        //    Assert.That(bankAccount.GetBalance(), Is.EqualTo(100));
        //}

        [Test]
        public void BankAccount_Add100_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message(""));

            BankAccount bankAccount = new BankAccount(logMock.Object);
            var result = bankAccount.Deposit(100);
            Assert.That(result, Is.True);
            Assert.That(bankAccount.GetBalance(), Is.EqualTo(100));
        }

        [Test]
        [TestCase(200,100)]
        [TestCase(200,150)]
        public void BankWithdraw_Withdraw100With200Balance_ReturnsTrue(int balance, int withdraw) 
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.LogToDb(It.IsAny<string>())).Returns(true);

            // Condition given in the moq that input should be greater than 0
            logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);

            BankAccount bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(balance);

            var result = bankAccount.Withdraw(withdraw);

            Assert.That(result, Is.True);
        }

        [Test]
        public void BankWithdraw_Withdraw300With200Balance_ReturnsFalse() 
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.LogToDb(It.IsAny<string>())).Returns(true);

            // Condition given in the moq that input should be greater than 0
            logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x => x < 0))).Returns(false);

            BankAccount bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(200);

            var result = bankAccount.Withdraw(300);

            Assert.That(result, Is.False);
        }
    }
}
