namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

            if ((automatedCar.CarTransmission == WorldObject.Transmission.D || automatedCar.CarTransmission == WorldObject.Transmission.N) && !automatedCar.KeyUpPressed && automatedCar.Throttle > 0)
            {
                automatedCar.Throttle-=0.5;
                automatedCar.MovementForward();
            }
            else if (automatedCar.CarTransmission == WorldObject.Transmission.R && !automatedCar.KeyUpPressed && automatedCar.Throttle > 0)
            {
                automatedCar.Throttle-=0.5;
                automatedCar.MovementBackward();
            }
            if (automatedCar.Brake > 0 && !automatedCar.KeyDownPressed)
            {
                automatedCar.Brake--; 
            }
            if (!automatedCar.KeyLeftPressed && automatedCar.Rotation >= -100&&automatedCar.Rotation<0)
            {
                automatedCar.Rotation++;
            }
            if (!automatedCar.KeyRightPressed && automatedCar.Rotation <= 100 && automatedCar.Rotation > 0)
            {
                automatedCar.Rotation--;
            }
        }


        
    }
}
