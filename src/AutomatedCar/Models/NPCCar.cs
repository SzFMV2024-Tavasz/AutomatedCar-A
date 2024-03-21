namespace AutomatedCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class NPCCar : AutomatedCar
    {
        
        public bool IsPedestrian { get; private set; } = false;

        private IRoute route;
        public IRoute Route
        {
            get { return route; }
            private set { }
        }

        public NPCCar(int x, int y, string filename, IRoute route) : base(x, y, filename)
        {
            this.route = route;
            if (route.IsPedestrian)
            {
                IsPedestrian = true;
            }
        }
    }
}
