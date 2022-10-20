using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace EmguCvDoodle
{
    static class Invoker
    {
        static public Image<Gray, Byte> Canny(ref Image<Bgr, Byte> canvas)
        {
            return canvas.Canny(50, 100);
        }

        static public void GaussianBlur(ref Image<Bgr, Byte> canvas)
        {
            canvas = canvas.SmoothGaussian(3);
        }
    }
}
