using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upr1
{
    public class Square : Rectangle
    {
        public Square(double A)
        {
           this.A = A;
        }

        public override double calculateCircumference()
        {
            return A * A;
        }

        public override double getArea()
        {
            return A * A;
        }

        public static double getStaticArea(double side)
        {
            return side * side;
        }
    }
}
