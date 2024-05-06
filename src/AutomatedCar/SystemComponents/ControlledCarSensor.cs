namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ControlledCarSensor : SystemComponent
    {
        //public AutomatedCar.Models.AutomatedCar controlledCar { get; protected set; }
        private ControlledCarPacket CarPacket { get; set; }
        public ControlledCarSensor(VirtualFunctionBus virtualFunctionBus) : base(virtualFunctionBus)
        {
            //this.controlledCar = controlledCar;
            this.CarPacket = new ControlledCarPacket();
            base.virtualFunctionBus.ControlledCarPacket = CarPacket;
            this.virtualFunctionBus.RegisterComponent(this);
        }

        public override void Process()
        {
            CarPacket.throttleCar = World.Instance.ControlledCar.Throttle;
            CarPacket.brakeCar = World.Instance.ControlledCar.Brake;
            CarPacket.kmhCar = World.Instance.ControlledCar.Velocity;
            CarPacket.SteeringWheelRotation = World.Instance.ControlledCar.SteeringWheelRotation;
            CarPacket.Transmission = (Packets.ControlledCarPacket.Transmissions)World.Instance.ControlledCar.CarTransmission;
            CarPacket.TransmissionL = (Packets.ControlledCarPacket.Transmissions)World.Instance.ControlledCar.CarTransmissionL;
            CarPacket.TransmissionR = (Packets.ControlledCarPacket.Transmissions)World.Instance.ControlledCar.CarTransmissionR;
            CarPacket.rpmCar = World.Instance.ControlledCar.Rpm;
            CarPacket.EmergencyBreakOnOff = World.Instance.ControlledCar.IsEmergencyBreakSafeWorking;
        }
    }
}
