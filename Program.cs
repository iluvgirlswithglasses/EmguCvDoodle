using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV;
using System;

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
            // ImageViewer.Show(canvas, "original");

            Image<Bgr, Byte> st = new Transform.Rotate(ref canvas).Create(Tool.Num.ToRad(60));
            ImageViewer.Show(st, "transformed");
        }
    }
}
