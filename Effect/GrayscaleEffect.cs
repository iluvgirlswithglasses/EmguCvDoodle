using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace EmguCvDoodle.Effect
{
    class GrayscaleEffect
    {
        private Image<Bgr, Byte> canvas;

        public GrayscaleEffect(ref Image<Bgr, Byte> c)
        {
            canvas = c;
        }

        public void Apply()
        {
            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                    canvas[y, x] = Tools.BgrTool.ToGray(canvas[y, x]);
        }
    }
}
