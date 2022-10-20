using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace EmguCvDoodle.Tool
{
    static class BgrTool
    {
        static public Bgr ToGray(Bgr v)
        {
            double res = (v.Red + v.Green + v.Blue) / 3.0;
            return new Bgr(res, res, res);
        }

        static public Bgr GrayToBgr(double v)
        {
            return new Bgr(v, v, v);
        }

        static public double Avg(Bgr v)
        {
            double res = (v.Red + v.Green + v.Blue) / 3.0;
            return res;
        }
    }
}
