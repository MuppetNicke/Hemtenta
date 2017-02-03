using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.webshop
{
    public class MyWebshop : IWebshop
    {
        public IBasket Basket { get; private set; }

        public MyWebshop(IBasket basket)
        {
            if (basket == null)
                throw new NullReferenceException();

            Basket = basket;
        }

        public void ResetCart()
        {
            Basket = new Basket();
        }

        public void Checkout(IBilling billing)
        {
            if (billing == null)
                throw new NullReferenceException();

            billing.Pay(Basket.TotalCost);
            //ResetCart();
        }
    }

    public class Bill : IBilling
    {
        public decimal Balance { get; set; }

        public void Pay(decimal amount)
        {
            if (Balance < amount)
                throw new InsufficientFundsException();

            Balance -= amount;
        }
    }
}
