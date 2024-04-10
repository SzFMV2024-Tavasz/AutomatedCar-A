using System;
using System.Linq;

namespace AutomatedCar.SystemComponents.Sensors.Helpers
{
    public class MapSign
    {
        public int MaxSpeed;
        public double Distance;

        public MapSign(string name, int carx, int cary, int sx, int sy)
        {
            this.MaxSpeed = MaxS(name);
            this.Distance = GetDistance(carx, cary, sx, sy);
        }

        private double GetDistance(int carx, int cary, int sx, int sy)
        {
            double dx = sx - carx;
            double dy = sy - cary;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        private int MaxS(string input)
        {
            // Get all the digits from the input string
            string digitsString = new string(input.Where(char.IsDigit).ToArray());

            if (!string.IsNullOrEmpty(digitsString))
            {
                // Convert the string of digits to an integer
                if (int.TryParse(digitsString, out int digits))
                {
                    return digits;
                }
                else
                {
                    throw new Exception("HIBA");
                }
            }
            else
            {
                if (input.Contains("stop"))
                {
                    return 0;
                }
            }

            return -1;
        }
    }
}