
namespace EmguCvDoodle.Type
{
    public class KeyPoint
    {
        public float Y { get; set; }
        public float X { get; set; }
        public double Intensity { get; set; }

        public KeyPoint(float y, float x, double intensity)
        {
            Y = y;
            X = x;
            Intensity = intensity;
        }

        public override string ToString()
        {
            string s = string.Format("{0} {1}, i = {2}", Y, X, Intensity);
            return s;
        }
    }
}
