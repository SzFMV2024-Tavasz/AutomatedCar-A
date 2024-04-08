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
                
                double rotationSpeed = (double)1.0;
                rotationDiff =Math.Abs(next.Rotation % 360 - car.Rotation % 360);
                  
               

                if (rotationDiff  > rotationSpeed )
                {
                    car.Rotation += rotationSpeed;
                }
                else if (rotationDiff > 0)
                {
                    car.Rotation = next.Rotation;
                }


                if (car.Route.RoutePoints[car.Route.CurrentPointID] == car.Route.RoutePoints.Last() && car.Route.RepeatAfterFinish)
                {
                    next = car.Route.RoutePoints[0];
                }
                else if (car.Route.RoutePoints[car.Route.CurrentPointID] != car.Route.RoutePoints.Last())
                {
                    next = car.Route.RoutePoints[car.Route.CurrentPointID + 1];
                }


                if (car.X==3510 && car.Y ==2490)
                {
                    ;
                }
            }
        }
    }
}
