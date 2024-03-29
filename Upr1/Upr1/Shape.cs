using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upr1
{
    public abstract class Shape
    {
        private int color;

        public abstract double calculateCircumference();

        public abstract double getArea();

        public void SetColor(int value)
        {
            switch (value)
            {
                case ShapeColor.RED:
                    color = Convert.ToInt32("11111111111111110000000000000000");
                    break;
                case ShapeColor.GREEN:
                    color = Convert.ToInt32("11111111000000001111111100000000");
                    break;
                case ShapeColor.BLUE:
                    color = Convert.ToInt32("11111111000000000000000011111111");
                    break;
                default:
                    throw new Exception("Value for color is not valid. Enter 1 [RED], 2 [GREEN] or 3 [BLUE]!");
            }
        }

        public int GetColor(int value)
        {
            string bytes = Convert.ToString(value, 2);
            switch (bytes)
            {
                case "11111111111111110000000000000000":
                    return ShapeColor.RED;
                case "11111111000000001111111100000000":
                    return ShapeColor.GREEN;
                case "11111111000000000000000011111111":
                    return ShapeColor.BLUE;
                default:
                    throw new ArgumentException("Value for color is not valid, please enter 1, 2 or 3!");
            }
        }

        private class ShapeColor
        {
            public const int RED = 1;
            public const int GREEN = 2;
            public const int BLUE = 3;
        }
    }
}
