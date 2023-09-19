using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhlClassLibrary
{
    public class Circle
    {
        private double _radius;

        public double Radius
        {
            get => _radius;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Radius), "Radius must be greater than zero.");
                }
                _radius = value;
            }
        }

        public Circle()
        {
            //_radius = -1;
            Radius = 1;
        }

        public Circle(double newRadius)
        {
            //_radius = newRadius;
            Radius = newRadius;
        }

        public double Area()
        {
            //return Math.PI * Radius * Radius;
            return Math.PI * _radius * _radius;
        }

        public double Perimeter()
        {
            return 2 * Math.PI * _radius;
        }
    }
}
