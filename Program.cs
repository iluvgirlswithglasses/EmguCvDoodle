using Emgu.CV.Structure;
using Emgu.CV;
using System;
using EmguCvDoodle.Detector;
using System.Collections.Generic;
using EmguCvDoodle.Type;

/*
deploy requirement:
    nuget packages:
        Emgu CV
        Emgu CV windows runtime
        Emgu CV UI
    build > configuration manager:
        Set platform to x64
*/

/*
some references for the future:
    https://ai.stanford.edu/~syyeung/cvweb/tutorial2.html
    http://matthewalunbrown.com/papers/ijcv2007.pdf
*/

namespace EmguCvDoodle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Image<Gray, byte> src = new Image<Gray, byte>("D:\\r\\siglaz\\EmguCvDoodle\\sample-images\\debian.png");
            Image<Gray, byte> cmp = new Image<Gray, byte>("D:\\r\\siglaz\\EmguCvDoodle\\sample-images\\debian-scaled.png");

            int lim = 100;

            List<KeyPoint> srcKeys = new HarrisCornerDetector(ref src).LibGetCorners(0.0001f, lim);
            List<KeyPoint> cmpKeys = new HarrisCornerDetector(ref cmp).LibGetCorners(0.0001f, lim);

            int matches = KeypointsMatching.Compare(ref srcKeys, ref cmpKeys);

            Console.WriteLine("{0} out of {1} keypoints matches", matches, lim);
        }
    }
}
