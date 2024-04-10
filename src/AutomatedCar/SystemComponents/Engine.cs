namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using AutomatedCar.Models;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

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
            if ((Packets.ControlledCarPacket.Transmissions)automatedCar.CarTransmission == Packets.ControlledCarPacket.Transmissions.D || (Packets.ControlledCarPacket.Transmissions)automatedCar.CarTransmission == Packets.ControlledCarPacket.Transmissions.N)
            {

                if (!automatedCar.KeyUpPressed || automatedCar.IsEmergencyBreakOn)
                {

                    if (automatedCar.Throttle >= 0.1)
                    {
                        automatedCar.Throttle -= 0.1;
                    }
                    if (automatedCar.Rpm > 0)
                    {
                        automatedCar.Rpm -= (automatedCar.Throttle);
                        if (automatedCar.Rpm < 0)
                        {
                            automatedCar.Rpm = 0;
                        }
                    }
                }
                automatedCar.MovementForward();
            }
            else
            {
                if ((Packets.ControlledCarPacket.Transmissions)automatedCar.CarTransmission == Packets.ControlledCarPacket.Transmissions.R)
                {
                    if (!automatedCar.KeyUpPressed || automatedCar.IsEmergencyBreakOn)
                    {

                        if (automatedCar.Throttle >= 0.1)
                        {
                            automatedCar.Throttle -= 0.1;
                        }
                        if (automatedCar.Rpm > 0)
                        {
                            automatedCar.Rpm -= (automatedCar.Throttle);
                            if (automatedCar.Rpm < 0)
                            {
                                automatedCar.Rpm = 0;
                            }
                        }
                    }
                    automatedCar.MovementBackward();
                }
            }
            if (automatedCar.Brake > 0 && !automatedCar.KeyDownPressed)
            {
                automatedCar.Brake--;
            }
            if (!automatedCar.KeyLeftPressed && automatedCar.SteeringWheelRotation >= -100 && automatedCar.SteeringWheelRotation < 0)
            {
                automatedCar.SteeringWheelRotation++;
            }
            if (!automatedCar.KeyRightPressed && automatedCar.SteeringWheelRotation <= 100 && automatedCar.SteeringWheelRotation > 0)
            {
                automatedCar.SteeringWheelRotation--;
            }

            if (automatedCar.IsEmergencyBreakOn)
            {
                  automatedCar.Brake = 100;
                automatedCar.SimulateBraking();
            }
        }

        
        
    }
}
