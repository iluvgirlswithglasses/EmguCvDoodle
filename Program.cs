using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV;
using System;
using EmguCvDoodle.Effect;

/*
deploy requirement:
    nuget packages:
        Emgu CV
        Emgu CV windows runtime
        Emgu CV UI
    build > configuration manager:
        Set platform to x64
*/

namespace EmguCvDoodle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Image<Bgr, Byte> canvas = new Image<Bgr, Byte>("D:\\r\\siglaz\\EmguCvDoodle\\sample-images\\eiffel.jpg");
            ImageViewer.Show(canvas, "original");

            new ContrastEffect(ref canvas).Apply(1.5, 0.5);
            ImageViewer.Show(canvas, "contrast");

            Image<Bgr, Byte> st = new Image<Bgr, Byte>(canvas.Height, canvas.Width);
            CvInvoke.Blur(
                canvas, st, 
                new System.Drawing.Size(5, 5), 
                new System.Drawing.Point(2, 2), 
                Emgu.CV.CvEnum.BorderType.Default
            );
            ImageViewer.Show(st, "blurred");

            // https://en.wikipedia.org/wiki/Affine_transformation
            Image<Bgr, Byte> nd = st.Rotate(45, new Bgr(0, 0, 0));
            ImageViewer.Show(nd, "rotation");
        }
    }
}
