namespace AutomatedCar.Models
{
    using global::AutomatedCar.SystemComponents;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
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
            private set { route = value; }
        }

        public NPCCar(int x, int y, string filename, IRoute route) : base(x, y, filename)
        {
            this.Route = route;
            if (route.IsPedestrian)
            {
                IsPedestrian = true;
                WorldObjectType = WorldObjectType.Pedestrian;
                
            }
            else
            {
                WorldObjectType = WorldObjectType.Car;
            }
            Collideable = true;
            new NPCMovingComponent(VirtualFunctionBus, this);
            new NPCRotatingComponent(VirtualFunctionBus, this);
            new NPCSpeedComponent(VirtualFunctionBus, this);
        }
    }
}
