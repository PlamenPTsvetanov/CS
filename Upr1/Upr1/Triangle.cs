using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upr1
{
    public class Triangle <T> where T : IComparable<T>
    {
        T side_1 { get; set; }
        T side_2 { get; set; }
        T side_3 { get; set; }

        public void getInstance(out Triangle<T> result)
        {
            result = new Triangle<T>(); 

            if (typeof(T) != typeof(int) 
                && typeof(T) != typeof(float))
            {
                throw new Exception("Type is not compatible!");
            }
            if (validTriangle())
            {
                Console.WriteLine("Triangle is valid!");
            } else
            {
                Console.WriteLine("Triangle is not valid!");
            }
        }

        private bool validTriangle()
        {
            if (
                (dynamic)side_1 + (dynamic)side_2 > (dynamic)side_3 &&
                (dynamic)side_2 + (dynamic)side_3 > (dynamic)side_1 &&
                (dynamic)side_1 + (dynamic)side_3 > (dynamic)side_2
                )
            {
                return true;
            }
           return false;
        }
    }
}
