using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemtenta_Niclas
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = int.MaxValue;
            int n = int.MaxValue;
            if (i + n >= int.MaxValue)
            {
                Console.WriteLine("TOO MACH");
            }
            else
                Console.WriteLine("OKE");

            Console.WriteLine("i: " + i);
            Console.WriteLine("n: " + n);
            Console.WriteLine("i+n: " + (int)(i + n));
        }
    }
}
