using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV;
using System;
using EmguCvDoodle.Detector;

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
            Image<Gray, byte> canvas = new Image<Gray, byte>("D:\\r\\siglaz\\EmguCvDoodle\\sample-images\\eiffel.jpg");
            canvas = canvas.SmoothGaussian(3);
            ImageViewer.Show(canvas);

            // Image<Gray, byte> sample = canvas.Copy();
            // new HarrisCornerDetector(ref sample).LibApply(0.0005f);
            // ImageViewer.Show(sample, "lib");
            new HarrisCornerDetector(ref canvas).Apply();
            ImageViewer.Show(canvas, "mine");
        }
    }
}
