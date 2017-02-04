using System;
using NUnit.Framework;
using HemtentaTdd2017;
using HemtentaTdd2017.webshop;

namespace UnitTest
{
    [TestFixture]
    public class WebShopTest
    {

        /// <summary>
        /// 
        /// 1. Vilka metoder och properties behöver testas?
        /// AddProduct(), RemoveProduct(), Webshop_Checkout(), IBasket Basket, decimal Balance.
        /// 
        /// 2. Ska några exceptions kastas?
        /// Exceptions som kastas är: NullReferenceException, BadPriceException, BadAmountException, InsufficientFundsException.
        /// 
        /// 3. Vilka är domänerna för IWebshop och IBasket?
        /// 
        /// IWebshop:
        /// Basket: Kan vara null eller ett tillåtet object av IBasket
        /// Checkout: Kan bara vara void
        /// 
        /// IBasket:
        /// AddProduct och RemoveProduct kan bara vara void
        /// TotalCost: decimaltal, MaxValue, MinValue, MinusOne, Zero, One
        /// 
        /// </summary>


        [Test]
        public void AddProduct_Succed()
        {
            Basket b = new Basket();
            Product p = new Product()
            {
                Name = "Hemtenta",
                Price = 100
            };

            b.AddProduct(p, 10);
            Assert.That(b.Products.Count, Is.EqualTo(10));
        }

        [Test]
        public void AddProduct_Fail_TooLargeValue()
        {
            Basket b = new Basket();
            Product p = new Product()
            {
                Name = "Hemtenta",
                Price = decimal.MaxValue
            };

            Assert.Throws<OverflowException>(() => b.AddProduct(p, 10));
        }

        [Test]
        public void AddProduct_Fail_NullReferenceException()
        {
            Basket b = new Basket();
            Product p = null;

            Assert.Throws<NullReferenceException>(() => b.AddProduct(p, 1));
        }

        [TestCase(0)]
        [TestCase(-11)]
        public void AddProduct_Fail_BadAmount_BadAmountException(int amount)
        {
            Basket b = new Basket();
            Product p = new Product()
            {
                Name = "Hemtenta",
                Price = 100
            };

            Assert.Throws<BadAmountException>(() => b.AddProduct(p, amount));
        }

        [TestCase(-1000)]
        [TestCase(-0.999999999999)]
        public void AddProduct_Fail_BadPrice_BadPriceException(decimal price)
        {
            Basket b = new Basket();
            Product p = new Product()
            {
                Name = "Hemtenta",
                Price = price
            };

            Assert.Throws<BadPriceException>(() => b.AddProduct(p, 1));
        }

        [Test]
        public void AddProduct_Fail_NullProductName_NullReferenceException()
        {
            Basket b = new Basket();
            Product p = new Product()
            {
                Name = null,
                Price = 100
            };

            Assert.Throws<NullReferenceException>(() => b.AddProduct(p, 1));
        }

        [Test]
        public void RemoveProduct_Succeed()
        {
            Basket b = new Basket();
            Product p = new Product()
            {
                Name = "Hemtenta",
                Price = 100
            };
            int amount = 10;

            b.AddProduct(p, amount);
            Assert.That(b.Products.Count, Is.EqualTo(amount));
            Assert.That(b.TotalCost, Is.EqualTo(p.Price * amount));

            amount = 5;
            b.RemoveProduct(p, amount);
            Assert.That(b.Products.Count, Is.EqualTo(amount));
            Assert.That(b.TotalCost, Is.EqualTo(p.Price * amount));
        }

        [Test]
        public void RemoveProduct_Fail_NullReferenceException()
        {
            Basket b = new Basket();
            Product p = null;

            Assert.Throws<NullReferenceException>(() => b.RemoveProduct(p, 1));
        }

        [TestCase(0)]
        [TestCase(-11)]
        public void RemoveProduct_Fail_BadAmount_BadAmountException(int amount)
        {
            Basket b = new Basket();
            Product p = new Product()
            {
                Name = "Hemtenta",
                Price = 100
            };

            Assert.Throws<BadAmountException>(() => b.RemoveProduct(p, amount));
        }

        [TestCase(-1000)]
        [TestCase(-0.999999999999)]
        public void RemoveProduct_Fail_BadPrice_BadPriceException(decimal price)
        {
            Basket b = new Basket();
            Product p = new Product()
            {
                Name = "Hemtenta",
                Price = price
            };

            Assert.Throws<BadPriceException>(() => b.RemoveProduct(p, 1));
        }

        [Test]
        public void RemoveProduct_Fail_NotEnoughInBasket_BadAmountException()
        {
            Basket b = new Basket();
            Product p = new Product()
            {
                Name = "Hemtenta",
                Price = 100
            };

            b.AddProduct(p, 5);

            Assert.Throws<BadAmountException>(() => b.RemoveProduct(p, 10));
        }

        [Test]
        public void RemoveProduct_Fail_NullProductName_NullReferenceException()
        {
            Basket b = new Basket();
            Product p = new Product()
            {
                Name = null,
                Price = 100
            };

            Assert.Throws<NullReferenceException>(() => b.RemoveProduct(p, 1));
        }

        [Test]
        public void Webshop_Fail_NullBasket_NullReferenceException()
        {
            MyWebshop mw;

            Assert.Throws<NullReferenceException>(() => mw = new MyWebshop(null));
        }

        [Test]
        public void Webshop_Checkout_Succeed()
        {
            Basket b = new Basket();
            Product p = new Product()
            {
                Name = "Hemtenta",
                Price = 100
            };

            MyWebshop mw = new MyWebshop(b);
            mw.Basket.AddProduct(p, 10);

            Bill bill = new Bill();
            bill.Balance = 1000;
            
            mw.Checkout(bill);
            mw.ResetCart();
            //mw.Basket.RemoveProduct(p, 10);

            Assert.That(bill.Balance, Is.EqualTo(0));
            Assert.That(mw.Basket.TotalCost, Is.EqualTo(0));
        }

        [Test]
        public void Webshop_Fail_Checkout_NotEnoughMoney_InsufficientFundsException()
        {
            Basket b = new Basket();
            Product p = new Product()
            {
                Name = "Hemtenta",
                Price = 100
            };

            MyWebshop mw = new MyWebshop(b);

            mw.Basket.AddProduct(p, 10);

            Bill bill = new Bill();
            bill.Balance = 500;

            Assert.Throws<InsufficientFundsException>(() => mw.Checkout(bill));
            mw.ResetCart();
            //mw.Basket.RemoveProduct(p, 10);
            Assert.That(mw.Basket.TotalCost, Is.EqualTo(0));
        }
    }
}
