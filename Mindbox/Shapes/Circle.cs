using System;

namespace Mindbox.Shapes
{
    public sealed class Circle : IShape
    {
        public readonly Point Center;
        public readonly float Radius;

        public Circle(Point center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public float Area => MathF.PI * Radius * Radius;

        public float Perimeter => Radius * 2 * MathF.PI;
    }
}
