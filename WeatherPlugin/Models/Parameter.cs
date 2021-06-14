namespace WeatherPlugin.Models
{
    public class Parameter
    {
        public string name { get; set; }
        public string levelType { get; set; }
        public int level { get; set; }
        public string unit { get; set; }
        public float[] values { get; set; }
    }
}