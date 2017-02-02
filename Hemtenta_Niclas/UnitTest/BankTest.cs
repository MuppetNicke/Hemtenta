using System;
using NUnit.Framework;
using HemtentaTdd2017;
using HemtentaTdd2017.bank;

namespace UnitTest
{
    [TestFixture]
    public class BankTest
    {
        [Test]
        public void Deposit_Fail_IllegalAmountException()
        {
            Account a = new Account();

            a.Deposit(500);
            a.Deposit(double.MaxValue);

            Assert.Throws<IllegalAmountException>(() => a.Deposit(double.MaxValue + 5));
        }

        [Test]
        public void Deposit_Fail_LessThanOne_IllegalAmountException()
        {
            Account a = new Account();

            Assert.Throws<IllegalAmountException>(() => a.Deposit(-5));
        }

        [Test]
        public void Deposit_Succeed()
        {
            Account a = new Account();

            a.Deposit(5);

            Assert.That(a.Amount, Is.EqualTo(5));
        }

        [Test]
        public void Withdraw_Fail_IllegalAmountException()
        {
            Account a = new Account();

            a.Deposit(500);

            Assert.Throws<IllegalAmountException>(() => a.Withdraw(double.MinValue));
        }

        [Test]
        public void Withdraw_Fail_LessThanOne_IllegalAmountException()
        {
            Account a = new Account();

            a.Deposit(5);

            Assert.Throws<IllegalAmountException>(() => a.Withdraw(-5));
        }

        [Test]
        public void Withdraw_Fail_InsufficientFundsException()
        {
            Account a = new Account();

            a.Deposit(3);

            Assert.Throws<InsufficientFundsException>(() => a.Withdraw(5));
        }

        [Test]
        public void Withdraw_Succeed()
        {
            Account a = new Account();

            a.Deposit(10);
            a.Withdraw(5);

            Assert.That(a.Amount, Is.EqualTo(5));
        }

        [Test]
        public void Transfer_Fail_MinValue()
        {
            Account a = new Account();
            Account a2 = new Account();

            a.Deposit(500);

            Assert.Throws<IllegalAmountException>(() => a.TransferFunds(a2, double.MinValue));
        }

        [Test]
        public void Transfer_Fail_MaxValue()
        {
            Account a = new Account();
            Account a2 = new Account();

            a.Deposit(double.MaxValue);

            Assert.Throws<IllegalAmountException>(() => a.TransferFunds(a2, double.MaxValue));
        }

        [Test]
        public void Transfer_Fail_NullDestination_NullReferenceException()
        {
            Account a = new Account();

            Assert.Throws<NullReferenceException>(() => a.TransferFunds(null, 200));
        }

        [Test]
        public void Transfer_Fail_InsufficientFundsException()
        {
            Account a = new Account();
            Account a2 = new Account();

            Assert.Throws<InsufficientFundsException>(() => a.TransferFunds(a2, 200));
        }

        [Test]
        public void Transfer_Fail_LessThanOne_IllegalAmountException()
        {
            Account a = new Account();
            Account a2 = new Account();

            a.Deposit(500);

            Assert.Throws<IllegalAmountException>(() => a.TransferFunds(a2, -5));
        }

    }
}
