using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upr1
{
    public class Rectangle : Shape, Ellipse
    {
        public double A { get; set; }
        public double B { get; set; }


        public Rectangle() { }
        public Rectangle(double A, double B)
        {
            this.A = A;
            this.B = B;
        }

        public override double calculateCircumference()
        {
            return 2 * (A + B);
        }

        public override double getArea()
        {
            return A * B;
        }

        public bool isEllipse()
        {
            return false;
        }
    }
}
