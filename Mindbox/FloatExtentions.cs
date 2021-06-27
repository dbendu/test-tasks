using System;

namespace Mindbox
{
    public static class FloatExtentions
    {
        /// <summary>
        /// Метод определяет, достаточно ли близки друг к другу два числа с плавающей запятой
        /// </summary>
        /// <param name="self"></param>
        /// <param name="to"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static bool CloseTo(this float self, float to, float epsilon = 1e-2f)
        {
            return MathF.Abs(self - to) < epsilon;
        }
    }
}
