using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV;
using System;
using EmguCvDoodle.Effect;
using EmguCvDoodle.Effect.Binary;

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

            // new GrayscaleEffect(ref canvas).Apply();
            new ContrastEffect(ref canvas).Apply(1.5, 0.5);
            Invoker.GaussianBlur(ref canvas);
            // ImageViewer.Show(canvas, "before binary");

            // new BinaryEffect(ref canvas).Apply(100.0);
            // ImageViewer.Show(canvas, "binary effect");
            ImageViewer.Show(canvas.Canny(50, 100), "Canny");

            // Image<Bgr, Byte> nd = canvas.Rotate(45, new Bgr(0, 0, 0));
            // ImageViewer.Show(nd, "rotation");
        }
    }
}
