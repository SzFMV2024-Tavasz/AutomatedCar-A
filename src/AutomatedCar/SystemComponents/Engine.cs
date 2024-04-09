namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using AutomatedCar.Models;
    public class Engine : SystemComponent
    {
        private VirtualFunctionBus virtualFunctionBus;
        private AutomatedCar automatedCar;
        
        public Engine(VirtualFunctionBus virtualFunctionBus,AutomatedCar automatedCar) : base(virtualFunctionBus)
        {
            this.virtualFunctionBus = virtualFunctionBus;
            virtualFunctionBus.RegisterComponent(this);
            this.automatedCar = automatedCar;
        }

        public override void Process()
        {
                if ((automatedCar.CarTransmission == WorldObject.Transmission.D || automatedCar.CarTransmission == WorldObject.Transmission.N)&& automatedCar.Throttle > 0)
                {
                    if (!automatedCar.KeyUpPressed)
                    {
                        automatedCar.Throttle -= 0.5;
                    }
                    automatedCar.MovementForward();
                }

            else 
            if (automatedCar.CarTransmission == WorldObject.Transmission.R  && automatedCar.Throttle > 0)
            {
                if (!automatedCar.KeyUpPressed)
                {
                    automatedCar.Throttle -= 0.5;
                }
                automatedCar.MovementBackward();
            }
            if (automatedCar.Brake > 0 && !automatedCar.KeyDownPressed)
            {
                automatedCar.Brake--; 
            }
            if (!automatedCar.KeyLeftPressed && automatedCar.SteeringWheelRotation >= -100&&automatedCar.SteeringWheelRotation<0)
            {
                automatedCar.SteeringWheelRotation++;
            }
            if (!automatedCar.KeyRightPressed && automatedCar.SteeringWheelRotation <= 100 && automatedCar.SteeringWheelRotation > 0)
            {
                automatedCar.SteeringWheelRotation--;
            }
        }


        
    }
}
