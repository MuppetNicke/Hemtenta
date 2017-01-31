using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.webshop
{
    class Basket : IBasket
    {
        public decimal TotalCost
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void AddProduct(Product p, int amount)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(Product p, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
