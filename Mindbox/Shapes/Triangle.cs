using System;

namespace Mindbox.Shapes
{
    public sealed class Triangle : IShape
    {
        public readonly Point A;
        public readonly Point B;
        public readonly Point C;

        public Triangle(Point a, Point b, Point c)
        {
            A = a;
            B = b;
            C = c;
        }

        public float Area
        {
            get
            {
                CalculateSidesLengths(out var ab, out var ac, out var bc);

                var p = (ab + ac + bc) / 2;

                return MathF.Sqrt(p * (p - ab) * (p - ac) * (p - bc));
            }
        }

        public float Perimeter
        {
            get
            {
                CalculateSidesLengths(out var ab, out var ac, out var bc);
                return ab + ac + bc;
            }
        }

        /// <summary>
        /// Метод определяет, является ли треугольник прямоугольным
        /// </summary>
        public bool Rectangular()
        {
            CalculateSidesLengths(out var ab, out var ac, out var bc);

            ab *= ab;
            ac *= ac;
            bc *= bc;

            var epsilon = 1e-5f;

            return (ab + ac).CloseTo(bc, epsilon) ||
                   (ab + bc).CloseTo(ac, epsilon) ||
                   (ac + bc).CloseTo(ab, epsilon);
        }

        private void CalculateSidesLengths(out float ab, out float ac, out float bc)
        {
            ab = Point.Distance(A, B);
            ac = Point.Distance(A, C);
            bc = Point.Distance(B, C);
        }
    }
}
