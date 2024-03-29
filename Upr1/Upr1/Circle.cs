using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upr1
{
    public class Circle : Shape, Ellipse
    {
        private double R;
        public Circle(double x)
        {
            this.R = x;
        }
        public override double calculateCircumference()
        {
            return 2 * Math.PI + R;
        }

        public override double getArea()
        {
            return Math.PI * R * R; 
        }

        public bool isEllipse()
        {
            return true;
        }
    }
}
