using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.webshop
{
    public class Basket : IBasket
    {

        public List<Product> Products = new List<Product>();

        public decimal TotalCost { get; private set; }

        public void AddProduct(Product p, int amount)
        {
            if (p == null)
                throw new NullReferenceException();

            if (string.IsNullOrWhiteSpace(p.Name))
                throw new NullReferenceException();

            if (p.Price < 0)
                throw new BadPriceException();

            if (amount < 1)
                throw new BadAmountException();

            for (int i = 0; i < amount; i++)
            {
                if (TotalCost + p.Price <= decimal.MaxValue)
                {
                    Products.Add(p);
                    TotalCost += p.Price;
                }
            }

        }

        public void RemoveProduct(Product p, int amount)
        {
            if (p == null)
                throw new NullReferenceException();

            if (string.IsNullOrWhiteSpace(p.Name))
                throw new NullReferenceException();

            if (p.Price < 0)
                throw new BadPriceException();

            if (amount < 1)
                throw new BadAmountException();

            int specificAmountProductInBasket = 0;

            foreach (Product product in Products)
            {
                if (product.Name.ToLower() == p.Name.ToLower() && product.Price == p.Price)
                    specificAmountProductInBasket++;
            }

            //Kollar ifall man försöker ta bort fler av något än vad som finns i korgen. Vet inte ifall denna checken behövs, men gör den ändå.
            if (amount > specificAmountProductInBasket)
                throw new BadAmountException();

            for(int i = 0; i < amount; i++)
            {
                Products.Remove(p);
                TotalCost -= p.Price;
            }
        }
    }
}
