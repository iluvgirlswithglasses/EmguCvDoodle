using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace EmguCvDoodle.Effect
{
    /*
    reference:
        https://en.wikipedia.org/wiki/Sobel_operator
    */

    class SobelOperator
    {
        private Image<Gray, byte> canvas;

        // just change the kernel and you got Canny's edge detech algorithm
        private float[,] kernel =
        {
            {-1, 0, +1 },
            {-2, 0, +2 },
            {-1, 0, +1 }
        };

        public SobelOperator(ref Image<Gray, byte> c)
        {
            canvas = c;
        }

        public Image<Gray, byte> LibCreate()
        {
            Image<Gray, byte> res = new Image<Gray, byte>(canvas.Size);
            Image<Gray, float> gx = new Image<Gray, float>(canvas.Size);
            CvInvoke.Sobel(canvas, gx, Emgu.CV.CvEnum.DepthType.Cv32F, 1, 0, 3);
            //
            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                    res[y, x] = new Gray(gx[y, x].Intensity);
            return res;
        }

        public void Apply()
        {
            Image<Gray, float> 
                src = canvas.Convert<Gray, float>(), 
                des = new Image<Gray, float>(canvas.Size);
            for (int y = 1; y < canvas.Height - 1; y++)
                for (int x = 1; x < canvas.Width - 1; x++)
                    ApplyMaskSafe(ref src, ref des, y, x);
            //
            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                    canvas[y, x] = new Gray(des[y, x].Intensity);
        }

        private void ApplyMaskSafe(ref Image<Gray, float> src, ref Image<Gray, float> des, int cy, int cx)
        {
            double res = 0.0;
            for (int i = 0; i <= 2; i++)
                for (int j = 0; j <= 2; j++)
                    res += src[cy - 1 + i, cx - 1 + j].Intensity * kernel[i, j];
            des[cy, cx] = new Gray(res);
        }
    }
}
