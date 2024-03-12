namespace AutomatedCar.Models
{
    public class Car : WorldObject
    {
        public Car(int x, int y, string filename)
            : base(x, y, filename)
        {
        }

        /// <summary>Gets or sets Speed in px/s.</summary>
        public double Speed { get; set; }
    }
}