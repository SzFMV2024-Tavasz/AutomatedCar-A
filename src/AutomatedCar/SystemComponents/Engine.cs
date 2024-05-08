namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets.Helpers.RelevantObjectHelper;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class Engine : SystemComponent
    {
        private VirtualFunctionBus virtualFunctionBus;
        private AutomatedCar automatedCar;
        
        public Engine(VirtualFunctionBus virtualFunctionBus,AutomatedCar automatedCar) : base(virtualFunctionBus)
        {
            this.virtualFunctionBus = virtualFunctionBus;
            //virtualFunctionBus.RegisterComponent(this);
            this.automatedCar = automatedCar;
        }

        public override void Process()
        {
            if ((Packets.ControlledCarPacket.Transmissions)automatedCar.CarTransmission == Packets.ControlledCarPacket.Transmissions.D || (Packets.ControlledCarPacket.Transmissions)automatedCar.CarTransmission == Packets.ControlledCarPacket.Transmissions.N)
            {

                if (!automatedCar.KeyUpPressed )//|| automatedCar.IsEmergencyBreakOn)
                {

                    if (automatedCar.Throttle > 0)
                    {
                        automatedCar.Throttle -= 0.1;
                        if (automatedCar.Throttle<0)
                        {
                            automatedCar.Throttle = 0;
                        }
                    }
                    if (automatedCar.Rpm > 0)
                    {
                        automatedCar.Rpm -= (automatedCar.Throttle +0.1)/5.5;
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
                    if (!automatedCar.KeyUpPressed )//|| automatedCar.IsEmergencyBreakOn)
                    {

                        if (automatedCar.Throttle > 0)
                        {
                            automatedCar.Throttle -= 0.1;
                            if (automatedCar.Throttle < 0)
                            {
                                automatedCar.Throttle =0;
                            }
                        }
                        if (automatedCar.Rpm > 0)
                        {
                            automatedCar.Rpm -= (automatedCar.Throttle + 0.1) / 5.5;
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
                automatedCar.Deccelerte();
                automatedCar.SimulateBraking();
            }

            WorldObject rObject = automatedCar.DetectObjInFrontOfTheCar();
            if (rObject!=null)
            {
                double distance= DistanceBetween(rObject,automatedCar);
                automatedCar.ObjectInFrontOfDistance = distance;
                automatedCar.CheckSafeDistance(distance);

            }
            else
            {
                automatedCar.ObjectInFrontOfDistance = 0;
                automatedCar.ActionRequiredFromDriver = false;
            }
            

        }
        private  double DistanceBetween(WorldObject from, WorldObject to)
        {
            return Math.Sqrt(Math.Pow(from.X - to.X, 2) + Math.Pow(from.Y - to.Y, 2));
        }

    }
}
