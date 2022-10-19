using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace EmguCvDoodle
{
    static class Invoker
    {
        static public Image<Bgr, Byte> Canny(ref Image<Bgr, Byte> canvas)
        {
            Image<Bgr, Byte> res = new Image<Bgr, Byte>(canvas.Width, canvas.Height);
            CvInvoke.Canny(canvas, res, 50, 200);
            return res;
        }
    }
}
