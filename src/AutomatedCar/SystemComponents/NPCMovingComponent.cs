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

        double leftX;
        double leftY;
        RoutePoint next;
        int xDiff;
        int yDiff;

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
            


            if (car.X != next.X)
            {
                if (car.X < next.X)
                {
                    double plusX = (double)car.Speed / GameBase.TicksPerSecond;
                    leftX += plusX - Math.Floor(plusX);
                    if (leftX > 1)
                    {
                        if (car.X + (int)Math.Floor(leftX) + (int)Math.Floor(plusX) <= next.X)
                        {
                            car.X += (int)Math.Floor(leftX) + (int)Math.Floor(plusX);
                        }
                        else
                        {
                            car.X = next.X;
                        }
                        leftX -= Math.Floor(leftX);
                    }
                    else
                    {
                        car.X += (int)Math.Floor(plusX);
                    }
                }
                else if (car.X > next.X)
                {
                    double plusX = (double)car.Speed / GameBase.TicksPerSecond;
                    leftX += plusX - Math.Floor(plusX);
                    if (leftX > 1)
                    {
                        if (car.X - (int)Math.Floor(leftX) - (int)Math.Floor(plusX) >= next.X)
                        {
                            car.X -= (int)Math.Floor(leftX) + (int)Math.Floor(plusX);
                        }
                        else
                        {
                            car.X = next.X;
                        }
                        leftX -= Math.Floor(leftX);
                    }
                    else
                    {
                        car.X -= (int)Math.Floor(plusX);
                    }
                }
                                
            }
            if (car.Y != next.Y)
            {
                if (car.Y < next.Y)
                {
                    double plusY = (double)car.Speed / GameBase.TicksPerSecond;
                    leftY += plusY - Math.Floor(plusY);
                    if (leftY > 1)
                    {
                        if (car.Y + (int)Math.Floor(leftY) + (int)Math.Floor(plusY) <= next.Y)
                        {
                            car.Y += (int)Math.Floor(leftY) + (int)Math.Floor(plusY);
                        }
                        else
                        {
                            car.Y = next.Y;
                        }
                        leftY -= Math.Floor(leftY);
                    }
                    else
                    {
                        car.Y += (int)Math.Floor(plusY);
                    }
                }
                else if (car.Y > next.Y)
                {
                    double plusY = (double)car.Speed / GameBase.TicksPerSecond;
                    leftY += plusY - Math.Floor(plusY);
                    if (leftY > 1)
                    {
                        if (car.Y - (int)Math.Floor(leftY) - (int)Math.Floor(plusY) >= next.Y)
                        {
                            car.Y -= (int)Math.Floor(leftY) + (int)Math.Floor(plusY);
                        }
                        else
                        {
                            car.Y = next.Y;
                        }
                        leftY -= Math.Floor(leftY);
                    }
                    else
                    {
                        car.Y -= (int)Math.Floor(plusY);
                    }
                }
              

            }
            if (car.X == next.X && car.Y == next.Y)
            {
                leftX = 0;
                leftY = 0;
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
                xDiff = next.X - car.X;
                yDiff = next.Y - car.Y;
            }



        }

    }
}
