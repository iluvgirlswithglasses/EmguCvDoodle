using System;
using Emgu.CV.Structure;

namespace EmguCvDoodle.Tools
{
    static class BgrTool
    {
        static public Bgr ToGray(Bgr v)
        {
            double res = (v.Red + v.Green + v.Blue) / 3.0;
            return new Bgr(res, res, res);
        }
    }
}
