namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    internal class NPCRotatingComponent : SystemComponent
    {
        NPCCar car;

        RoutePoint next;

        double rotationDiff;

        public NPCRotatingComponent(VirtualFunctionBus virtualFunctionBus,NPCCar car) : base(virtualFunctionBus)
        {
            this.car = car;
            next = car.Route.RoutePoints[car.Route.StartPointID + 1];
        }

        public override void Process()
        {
            
            
               
            if (car.IsPedestrian)
            {
                if (car.X == car.Route.RoutePoints[car.Route.CurrentPointID].X && car.Y == car.Route.RoutePoints[car.Route.CurrentPointID].Y)
                {
                    car.Rotation = car.Route.RoutePoints[car.Route.CurrentPointID].Rotation;
                }
            }
            else
            {
                // the speed/3 is kind of a placeholder,need to figure out what to put to it's place 
                double rotationSpeed = (double)( car.Route.RoutePoints[car.Route.CurrentPointID].Speed/3) / GameBase.TicksPerSecond;
                rotationDiff =Math.Abs(next.Rotation - car.Rotation);

                if (rotationDiff < 1)
                {
                    
                    if (car.Route.RoutePoints[car.Route.CurrentPointID] == car.Route.RoutePoints.Last() && car.Route.RepeatAfterFinish)
                    {
                        next = car.Route.RoutePoints[0];
                    }
                    else if (car.Route.RoutePoints[car.Route.CurrentPointID] == car.Route.RoutePoints.Last())
                    {
                        car.Stop();
                    }
                    else
                    {
                        next = car.Route.RoutePoints[car.Route.CurrentPointID + 1];
                    }
                }
                else
                {
                    car.Rotation +=rotationSpeed;
                    
                }
            }
        }
    }
}
