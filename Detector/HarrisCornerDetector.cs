using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace EmguCvDoodle.Detector
{
    /*
    references:
        https://en.wikipedia.org/wiki/Harris_corner_detector
        https://muthu.co/harris-corner-detector-implementation-in-python/
        https://viblo.asia/p/ung-dung-thuat-toan-harris-corner-detector-trong-bai-toan-noi-anh-phan-i-ByEZkyME5Q0
        https://github.com/Daksh-404/Harris-corner-detector
    */

    class HarrisCornerDetector
    {
        private Image<Gray, byte> canvas;

        public HarrisCornerDetector(ref Image<Gray, byte> c)
        {
            canvas = c;
        }

        public void Apply(float threshold = 1e9f)
        {
            Image<Gray, float>
                gradX = new Image<Gray, float>(canvas.Size),
                gradY = gradX.Copy(),
                x2Derivative = gradX.Copy(),
                y2Derivative = gradX.Copy(),
                xyDerivative = gradX.Copy(),
                x2y2Derivative = gradX.Copy(),
                det = gradX.Copy(),
                trace = gradX.Copy(),
                uncleansed = gradX.Copy(),
                res = gradX.Copy();

            //
            CvInvoke.Sobel(canvas, gradX, Emgu.CV.CvEnum.DepthType.Cv32F, 1, 0, 3); // dx=1, dy=0, ksize=3
            CvInvoke.Sobel(canvas, gradY, Emgu.CV.CvEnum.DepthType.Cv32F, 0, 1, 3); // dx=0, dy=1, ksize=3
            //
            CvInvoke.Multiply(gradX, gradX, x2Derivative);
            CvInvoke.Multiply(gradY, gradY, y2Derivative);
            CvInvoke.Multiply(gradX, gradY, xyDerivative);
            //
            x2Derivative = x2Derivative.SmoothGaussian(3);
            y2Derivative = y2Derivative.SmoothGaussian(3);
            xyDerivative = xyDerivative.SmoothGaussian(3);
            // det = x2*y2 - (xy)2
            CvInvoke.Multiply(x2Derivative, y2Derivative, det);
            CvInvoke.Multiply(xyDerivative, xyDerivative, x2y2Derivative);
            CvInvoke.Subtract(det, x2y2Derivative, det);
            // trace = x2 + y2
            CvInvoke.Add(x2Derivative, y2Derivative, trace);
            // trace = k * (trace**2)
            CvInvoke.Multiply(trace, trace, trace);
            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                    trace[y, x] = new Gray(trace[y, x].Intensity * 0.04);
            //
            // CvInvoke.Subtract(det, trace, uncleansed);
            // clean(ref uncleansed, ref res, threshold);
            CvInvoke.Subtract(det, trace, res);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                    if (res[y, x].Intensity > threshold)
                        canvas[y, x] = new Gray(255);
                    else
                        canvas[y, x] = new Gray(0);
            
        }

        public void LibApply(float threshold)
        {
            Image<Gray, float> cornerImg = new Image<Gray, float>(canvas.Size);
            CvInvoke.CornerHarris(canvas, cornerImg, 3, 3, 0.04);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                    if (cornerImg[y, x].Intensity > threshold)
                        canvas[y, x] = new Gray(255);
                    else
                        canvas[y, x] = new Gray(0);
        }

        public List<Point> LibGetCorners(float threshold)
        {
            List<Point> ls = new List<Point>();
            Image<Gray, float> cornerImg = new Image<Gray, float>(canvas.Size);
            CvInvoke.CornerHarris(canvas, cornerImg, 3, 3, 0.04);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                    if (cornerImg[y, x].Intensity > threshold)
                        ls.Add(new Point(x, y));

            return ls;
        }

        // only keep the corners that matter the most
        private void clean(ref Image<Gray, float> src, ref Image<Gray, float> des, float threshold)
        {
            for (int y = 1; y < src.Height - 1; y++)
                for (int x = 1; x < src.Width - 1; x++)
                    if (src[y, x].Intensity > threshold && src[y, x].Intensity >= findMax(ref src, y, x))
                        des[y, x] = src[y, x];
        }

        private float findMax(ref Image<Gray, float> src, int cy, int cx)
        {
            float mx = -1e19f;
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    mx = Math.Max(mx, (float) src[cy + i, cx + j].Intensity);
            return mx;
        }
    }
}
