using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace physica.ang
{
    public struct Angle
    {
        float angle;
        public Angle(float a)
        {
            this.angle = a;
        }
        public static Angle operator +(Angle a, Angle b)
        {
            return new Angle(a.angle + b.angle);
        }
        public static Angle operator -(Angle a, Angle b)
        {
            return new Angle(a.angle - b.angle);
        }
        public static Angle operator /(Angle a, Angle b)
        {
            return new Angle(a.angle / b.angle);
        }
        public static Angle operator *(Angle a, Angle b)
        {
            return new Angle(a.angle * b.angle);
        }
        public static Angle operator *(Angle a, float b)
        {
            return new Angle(a.angle * b);
        }
        public string ToString(bool rounded = false, bool descript = false)
        {
            if (rounded)
            {
                return $"{(int)Math.Round(angle)}";
            }
            else
            {
                return ToString();
            }
        }
    }
}
