namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using ExCSS;
    using SkiaSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class NPCMovingComponent : SystemComponent
    {
        NPCCar car; 

        public NPCMovingComponent(VirtualFunctionBus virtualFunctionBus, NPCCar car) 
            : base(virtualFunctionBus)
        {
            this.car = car;
            next = car.Route.RoutePoints[car.Route.StartPointID + 1];
        }

        RoutePoint next;

        int sqrt2;

        public override void Process()
        {
            // this part sets the next RoutePoint, and checks if the car's route should repeat after finish or not.


            if (car.Speed < 0)
            {
                car.Stop();
            }
            if (car.X != next.X  && car.Y != next.Y && sqrt2 == 3)
            {
                sqrt2 = 0;
                return;
            }
            else if (car.X != next.X && car.Y != next.Y)
            {
                sqrt2++;
            }

            double plusX = (double)car.Speed / GameBase.TicksPerSecond;

            if (car.X != next.X)
            {
                if (car.X < next.X)
                {

                    if (car.X + plusX <= next.X)
                    {
                        car.X += plusX;
                    }
                    else
                    {
                        car.X = next.X;
                    }
                }
                else if (car.X > next.X)
                {

                    if (car.X - plusX >= next.X)
                    {
                        car.X -= plusX;
                    }
                    else
                    {
                        car.X = next.X;
                    }
                }
                                
            }
            if (car.Y != next.Y)
            {
                if (car.Y < next.Y)
                {

                    if (car.Y + plusX <= next.Y)
                    {
                        car.Y += plusX;
                    }
                    else
                    {
                        car.Y = next.Y;
                    }
                }
                else if (car.Y > next.Y)
                {

                    if (car.Y - plusX >= next.Y)
                    {
                        car.Y -= plusX;
                    }
                    else
                    {
                        car.Y = next.Y;
                    }
                }

            }
            if (car.X == next.X && car.Y == next.Y)
            {

                if (car.Route.RoutePoints[car.Route.CurrentPointID + 1] != car.Route.RoutePoints.Last())
                {
                    car.Route.CurrentPointID = car.Route.RoutePoints.IndexOf(car.Route.RoutePoints.Single(t => t == next));
                }
                
                if (car.Route.RoutePoints[car.Route.CurrentPointID+1] == car.Route.RoutePoints.Last() && car.Route.RepeatAfterFinish)
                {
                    next = car.Route.RoutePoints[0];
                }
                else if (car.Route.RoutePoints[car.Route.CurrentPointID] != car.Route.RoutePoints.Last())
                {
                    next = car.Route.RoutePoints[car.Route.CurrentPointID + 1];
                }

            }



        }

    }
}
