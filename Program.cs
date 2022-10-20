using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV;
using System;
using EmguCvDoodle.Detector;
using System.Drawing;

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
            Image<Gray, byte> canvas = new Image<Gray, byte>("D:\\r\\siglaz\\EmguCvDoodle\\sample-images\\reimu.jpg");
            canvas = canvas.SmoothGaussian(3);

            ImageViewer.Show(new Effect.SobelOperator(ref canvas).LibCreate());
            new Effect.SobelOperator(ref canvas).Apply();
            ImageViewer.Show(canvas);
        }
    }
}
