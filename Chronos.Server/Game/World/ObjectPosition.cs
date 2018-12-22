using Chronos.Protocol.Types;
using System;

namespace Chronos.Server.Game.World
{
    public class ObjectPosition
    {
        public ObjectPosition(Map map, float x, float y, float z)
        {
            Map = map;
            X = x;
            Y = y;
            Z = z;
        }
        public ObjectPosition(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Map Map { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float DistanceTo(ObjectPosition to)
        {
            return (float)(Math.Pow(this.X - to.X, 2) + Math.Pow(this.Y - to.Y, 2) + Math.Pow(this.Z - to.Z, 2));
        }

        public bool IsInRange(ObjectPosition to, int range)
        {
            float distance = DistanceTo(to);
            return distance <= Math.Pow(range, 2);
        }

        public bool IsInCircle(ObjectPosition other, float radius)
        {
            float xDistance = this.X - other.X;
            float zDistance = this.Z - other.Z;

            return (Math.Pow(xDistance, 2) + Math.Pow(zDistance, 2)) <= Math.Pow(radius, 2);
        }
        public PositionType GetNetwork()
        {
            return new PositionType(X, Y, Z);
        }

    }
}