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

        public const int EmergencyBrake = 2; //2 a vészfék
        public override void Process()
        {
            double CarFinalSpeed = car.Route.RoutePoints[car.Route.CurrentPointID].Speed; 
                //car.Route.RoutePoints[car.Route.CurrentPointID].Speed;
            if (CarCurrentSpeed < CarFinalSpeed )
            {
                helper = helper + 0.1;
                if ( helper == 1 ) 
                {
                    CarCurrentSpeed = CarCurrentSpeed + 1;
                    car.Speed = CarCurrentSpeed;
                    helper = 0;
                }
            }
            else if (CarCurrentSpeed > CarFinalSpeed)
            {
                helper = helper + 0.1;
                if (helper == 1)
                {
                    CarCurrentSpeed = CarCurrentSpeed - 1;
                    car.Speed = CarCurrentSpeed;
                    helper = 0;
                }
            }
            else if (CarFinalSpeed == 0 && car.Route.RepeatAfterFinish == false)
            {
                if (CarCurrentSpeed >= EmergencyBrake)
                {
                    helper += 0.1;
                    if (helper == 2)
                    {
                        CarCurrentSpeed -= EmergencyBrake;
                        car.Speed = CarCurrentSpeed;
                        helper = 0;
                    }
                }
                else 
                {
                    CarCurrentSpeed = 0;
                    car.Speed = CarCurrentSpeed;
                }
            }
        }
    }
}
