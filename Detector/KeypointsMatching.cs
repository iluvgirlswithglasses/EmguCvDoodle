using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using EmguCvDoodle.Tool;
using EmguCvDoodle.Type;

namespace EmguCvDoodle.Detector
{
    /*
    take two images
    response whether these are the same image or not

    references:
        https://stackoverflow.com/questions/843972/image-comparison-fast-algorithm
        https://stackoverflow.com/questions/35194681/how-to-compare-two-group-keypoint-in-opencv
        https://ai.stanford.edu/~syyeung/cvweb/tutorial2.html
        me
    */
    static class KeypointsMatching
    {
        static public void LibCompare(ref Image<Gray, byte> src, ref Image<Gray, byte> cmp)
        {
            var orb = new ORB();

            var keypts = new VectorOfKeyPoint();
            var descSrc = new Image<Gray, byte>(src.Size);
            orb.DetectAndCompute(src, null, keypts, descSrc, false);

            var keypts2 = new VectorOfKeyPoint();
            var descCmp = new Image<Gray, byte>(src.Size);
            orb.DetectAndCompute(cmp, null, keypts2, descCmp, false);

            // brute force matching
            var bf = new BFMatcher(DistanceType.Hamming, true);
            var matches = new VectorOfDMatch();
            bf.Match(descSrc, descCmp, matches);

            // debuging
            Console.WriteLine("st keys: {0}, nd keys: {1}, matches: {2}", keypts.Size, keypts2.Size, matches.Size);
        }

        static public int Compare(ref List<KeyPoint> st, ref List<KeyPoint> nd, double radThresh = 0.0005, double lenThresh = 0.0005)
        {
            int matches = 0;

            rotate(ref st);
            rotate(ref nd);

            List<bool> used = Enumerable.Repeat(false, nd.Count).ToList();

            foreach (KeyPoint p in st)
            {
                for (int i = 0; i < nd.Count; i++) if (!used[i])
                {
                    if (1 - calcCos(p, nd[i]) < radThresh)
                    {
                        used[i] = true;
                        matches++;
                    }
                }
            }

            return matches;
        }

        static private void rotate(ref List<KeyPoint> ls)
        {
            double rad = calcAlphaToOx(ls[0]);
            if (ls[0].Y < 0)
                rad = Num.PI * 2 - rad;
            double d = calcDistance(ls[0]);
            // xoay cả cái list sao cho điểm có intensity lớn nhất
            // nằm ở góc 12 giờ
            for (int i = 0; i < ls.Count; i++)
            {
                double dy = - ls[i].X * Math.Sin(rad) + ls[i].Y * Math.Cos(rad),
                       dx = ls[i].X * Math.Cos(rad) + ls[i].Y * Math.Sin(rad);
                ls[i].Y = (float) dy;
                ls[i].X = (float) dx;
                // temporarily change intensity to the ratio between
                // its length and the anchor's length
                ls[i].Intensity = calcDistance(ls[i]) / d;
            }
        }

        static private void rotate(ref KeyPoint p, double rad)
        {
            double dy = p.X * Math.Sin(rad) + p.Y * Math.Cos(rad),
                   dx = p.X * Math.Cos(rad) - p.Y * Math.Sin(rad);
            p.Y = (float) dy;
            p.X = (float) dx;
        }

        static private double calcCos(KeyPoint u, KeyPoint v)
        {
            double _cos = (u.X * v.X) + (u.Y * v.Y);
            _cos /= (calcDistance(u) * calcDistance(v));
            return _cos;
        }

        static private double calcAlphaToOx(KeyPoint u)
        {
            return Math.Acos(u.X / Math.Sqrt(u.X * u.X + u.Y * u.Y));
        }

        static private double calcDistance(KeyPoint u)
        {
            return Math.Sqrt(u.X * u.X + u.Y * u.Y);
        }
    }
}
