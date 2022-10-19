using System;
using static System.Math;

namespace EmguCvDoodle.Tool
{
    class Num
    {
        public static double PI = Acos(0.0) * 2.0;

        public static double ToRad(double a)
        {
            return a / 180.0 * PI;
        }
    }
}
