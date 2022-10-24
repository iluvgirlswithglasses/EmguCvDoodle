using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV;
using System;
using EmguCvDoodle.Detector;
using System.Drawing;
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
            Image<Gray, byte> canvas = new Image<Gray, byte>("D:\\r\\siglaz\\EmguCvDoodle\\sample-images\\debian.png");
            
            canvas = canvas.SmoothGaussian(3);
            ImageViewer.Show(canvas, "src");

            new HarrisCornerDetector(ref canvas).LibApply(0.001f);
            ImageViewer.Show(canvas);
        }
    }
}
