using System;

namespace Mindbox
{
    /// <summary>
    /// Точка на декартовых координатах.
    /// </summary>
    public sealed class Point
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        public Point(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static float Distance(in Point a, in Point b)
        {
            var xDist = b.X - a.X;
            var yDist = b.Y - a.Y;
            var zDist = b.Z - a.Z;

            return Math.Abs(MathF.Sqrt(xDist * xDist +
                                       yDist * yDist +
                                       zDist * zDist)
                           );
        }
    }
}
