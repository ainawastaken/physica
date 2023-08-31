using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using physica.vec;
using physica.ang;
using physica.poly;


namespace physica.physics
{
    public struct physicsObject
    {
        public Vector location;
        public Angle angle;

        public Vector velocity;
        public float angularVelocity;
        public Vector gravity;

        public Polygon shape;
        private Polygon realShape;


    }
}
