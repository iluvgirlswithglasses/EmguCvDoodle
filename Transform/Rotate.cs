using System;
using Emgu.CV;
using Emgu.CV.Structure;
using EmguCvDoodle.Tool;

namespace EmguCvDoodle.Transform
{
    /*
    references:
        https://en.wikipedia.org/wiki/Affine_transformation
        https://en.wikipedia.org/wiki/Transformation_matrix#Rotation
        https://www.algorithm-archive.org/contents/affine_transformations/affine_transformations.html

    the idea of the matrix given in wiki is that:
        given a point P, 
        after multiplying it with the matrix M, 
        we got P' which is the point after rotation

    so with every point P(y, x), after transformation it becomes P'(y', x') where:
        y' = x * sin(a) + y * cos(a)
        x' = x * cos(a) - y * sin(a)
    */

    class Rotate
    {
        private Image<Bgr, Byte> canvas;

        public Rotate(ref Image<Bgr, Byte> c)
        {
            canvas = c;
        }

        public Image<Bgr, Byte> Create(double rad)
        {
            Image<Bgr, Byte> res = new Image<Bgr, Byte>(
                canvas.Width, canvas.Height, new Bgr(0, 0, 0)
            );
            int cy = canvas.Height >> 1, cx = canvas.Width >> 1;
            //
            for (int y = 0; y < canvas.Height; y++)
            {
                for (int x = 0; x < canvas.Width; x++)
                {
                    // given P', calculate P

                    double py = -cy + y, px = -cx + x;
                    int dy = (int) Math.Round(- px * Math.Sin(rad) + py * Math.Cos(rad) + cy),
                        dx = (int) Math.Round(px * Math.Cos(rad) + py * Math.Sin(rad) + cx);

                    if (0 <= dy && dy < canvas.Height && 0 <= dx && dx < canvas.Width)
                        res[y, x] = canvas[dy, dx];
                }
            }
            //
            return res;
        }
    }
}
