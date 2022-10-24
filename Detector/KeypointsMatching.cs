using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace EmguCvDoodle.Detector
{
    /*
    take two images
    response whether these are the same image or not

    references:
        https://stackoverflow.com/questions/843972/image-comparison-fast-algorithm
        https://stackoverflow.com/questions/35194681/how-to-compare-two-group-keypoint-in-opencv
        me
    */
    static class KeypointsMatching
    {

        static public bool Compare(Image<Gray, byte> st, Image<Gray, byte> nd, float threshold)
        {
            List<Point> u = new HarrisCornerDetector(ref st).LibGetCorners(threshold);
            List<Point> v = new HarrisCornerDetector(ref nd).LibGetCorners(threshold);
            //
            Point uc = new Point(0, 0), vc = new Point(0, 0);
            foreach (Point p in u)
            {
                uc.X += p.X; 
                uc.Y += p.Y;
            }
            foreach (Point p in v)
            {
                vc.X += p.X;
                vc.Y += p.Y;
            }
            //
            return false;
        }
    }
}
