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
        
        public NPCSpeedComponent(VirtualFunctionBus virtualFunctionBus,NPCCar car) : base(virtualFunctionBus)
        {
            this.car = car;
        }

        public const int EmergencyBrake = 3; //3 a vészfék
        public override void Process()
        {
            double CarFinalSpeed = car.Route.RoutePoints[car.Route.CurrentPointID].Speed; 
                //car.Route.RoutePoints[car.Route.CurrentPointID].Speed;
            if (CarCurrentSpeed < CarFinalSpeed )
            {
                CarCurrentSpeed = CarCurrentSpeed + 1;
                car.Speed = CarCurrentSpeed;
            }
            else if (CarCurrentSpeed > CarFinalSpeed)
            {
                CarCurrentSpeed = CarCurrentSpeed - 1;
                car.Speed = CarCurrentSpeed;
            }
            else if (CarFinalSpeed == 0 && car.Route.RepeatAfterFinish == false)
            {
                if (CarCurrentSpeed >= EmergencyBrake)
                {
                    CarCurrentSpeed -= EmergencyBrake;
                    car.Speed = CarCurrentSpeed;
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
