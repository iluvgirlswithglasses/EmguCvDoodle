using System;
using Emgu.CV.Structure;
using Emgu.CV;

namespace EmguCvDoodle.Effect.Binary
{
    class BinaryEffect
    {
        private Image<Bgr, Byte> canvas;

        public BinaryEffect(ref Image<Bgr, Byte> c)
        {
            canvas = c;
        }

        public void Apply(double threshold)
        {
            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                    if (Tool.BgrTool.Avg(canvas[y, x]) < threshold)
                        canvas[y, x] = Tool.BgrTool.GrayToBgr(0);
                    else
                        canvas[y, x] = Tool.BgrTool.GrayToBgr(255);
        }
    }
}
