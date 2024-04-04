namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class NPCSpeedComponent : SystemComponent
    {
        NPCCar car;
        private int CarCurrentSpeed = 0;
        private double helper = 0;
        
        public NPCSpeedComponent(VirtualFunctionBus virtualFunctionBus,NPCCar car) : base(virtualFunctionBus)
        {
            this.car = car;
        }

        public const int EmergencyBrake = 1; //2 a vészfék
        public override void Process()
        {
                //car.Route.RoutePoints[car.Route.CurrentPointID].Speed;
            //if (CarCurrentSpeed < CarNextSpeed )
            //{
            //    helper = helper + 0.1;
            //    if ( helper == 1 ) 
            //    {
            //        CarCurrentSpeed = CarCurrentSpeed + 1;
            //        car.Speed = CarCurrentSpeed;
            //        helper = 0;
            //    }
            //}
            //else if (CarCurrentSpeed > CarNextSpeed)
            //{
            //    helper = helper + 0.1;
            //    if (helper == 1)
            //    {
            //        CarCurrentSpeed = CarCurrentSpeed - 1;
            //        car.Speed = CarCurrentSpeed;
            //        helper = 0;
            //    }
            //}
            //else if (CarNextSpeed == 0 && car.Route.RepeatAfterFinish == false)
            //{
            //    if (CarCurrentSpeed >= EmergencyBrake)
            //    {
            //        helper += 0.1;
            //        if (helper == 2)
            //        {
            //            CarCurrentSpeed -= EmergencyBrake;
            //            car.Speed = CarCurrentSpeed;
            //            helper = 0;
            //        }
            //    }
            //    else 
            //    {
            //        CarCurrentSpeed = 0;
            //        car.Speed = CarCurrentSpeed;
            //    }
            //}
            
            var Next = car.Route.RoutePoints[car.Route.CurrentPointID + 1];
            if (Next != car.Route.RoutePoints.Last())
            {
                if (CarCurrentSpeed < Next.Speed)
                {
                    helper = helper + 0.1;
                    if (Math.Round(helper, 1) == 0.8)
                    {
                        CarCurrentSpeed = CarCurrentSpeed + 1;
                        car.Speed = ConvertToPxS(CarCurrentSpeed);
                        helper = 0;
                    }
                }
                else if (CarCurrentSpeed > Next.Speed)
                {
                    helper = helper + 0.1;
                    if (Math.Round(helper, 1) == 0.2)
                    {
                        CarCurrentSpeed = CarCurrentSpeed - 2;
                        car.Speed = ConvertToPxS(CarCurrentSpeed);
                        helper = 0;
                    }
                }
            }
            else if(Next == car.Route.RoutePoints.Last() && car.Route.RepeatAfterFinish == true)
            {
                Next = car.Route.RoutePoints[0];
                if (CarCurrentSpeed < Next.Speed)
                {
                    helper = helper + 0.1;
                    if (Math.Round(helper,1) == 0.8)
                    {
                        CarCurrentSpeed = CarCurrentSpeed + 1;
                        car.Speed = ConvertToPxS(CarCurrentSpeed);
                        helper = 0;
                    }
                }
                else if (CarCurrentSpeed > Next.Speed)
                {
                    helper = helper + 0.1;
                    if (Math.Round(helper,1) == 0.2)
                    {
                        CarCurrentSpeed = CarCurrentSpeed - 2;
                        car.Speed = ConvertToPxS(CarCurrentSpeed);
                        helper = 0;
                    }
                }
            }
            else if (Next == car.Route.RoutePoints.Last() && car.Route.RepeatAfterFinish == false)
            {
                helper = helper + 0.1;
                if (Math.Round(helper, 1) == 0.3)
                {
                    car.Speed -= ConvertToPxS(EmergencyBrake);
                    helper = 0;
                    
                }
            }

        }

        private int ConvertToPxS(double value)
        {
            var changer = (int)Math.Round((value / 3.6) * 50,0);
            return changer;
        }

    }
}
