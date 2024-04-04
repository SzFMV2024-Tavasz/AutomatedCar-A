namespace AutomatedCar.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RoutePoint
    {
        [JsonProperty("X")]
        public int X { get; }
        [JsonProperty("Y")]
        public int Y { get; }

        [JsonProperty("Rotation")]
        public double Rotation { get; }

        [JsonProperty("Speed")]
        public int Speed { get; }
        public RoutePoint(int x, int y, double rotation, int speed)
        {
            X = x;
            Y = y;
            Rotation = rotation;
            Speed = speed;
        }
    }
}
