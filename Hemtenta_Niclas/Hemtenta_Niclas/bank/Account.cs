using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.bank
{
    public class Account : IAccount
    {
        public double Amount { get; private set; }

        public void Deposit(double amount)
        {
            if (amount < 1 || amount > double.MaxValue)
                throw new IllegalAmountException();

            if (Amount + amount < 1)
                throw new IllegalAmountException();

            if (double.IsInfinity(Amount + amount))
                throw new IllegalAmountException();


            Amount = Amount + amount;
        }

        public void TransferFunds(IAccount destination, double amount)
        {
            if (destination == null)
                throw new NullReferenceException();

            if (Amount == 0)
                throw new InsufficientFundsException();

            if (Amount - amount < 0)
                throw new InsufficientFundsException();

            if (amount < 0 || amount > double.MaxValue)
                throw new IllegalAmountException();

            if (double.IsInfinity(destination.Amount + amount))
                throw new OperationNotPermittedException();

            Amount -= amount;
            destination.Deposit(amount);
        }

        public void Withdraw(double amount)
        {
            if (Amount == 0)
                throw new InsufficientFundsException();

            if (Amount - amount < 0)
                throw new InsufficientFundsException();

            if (amount < 0 || amount > double.MaxValue)
                throw new IllegalAmountException();

            Amount -= amount;
        }
    }
}
