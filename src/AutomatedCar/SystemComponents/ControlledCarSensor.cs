namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.SystemComponents.Packets;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ControlledCarSensor : SystemComponent
    {
        public AutomatedCar.Models.AutomatedCar controlledCar { get; protected set; }
        private ControlledCarPacket CarPacket;
        public ControlledCarSensor(VirtualFunctionBus virtualFunctionBus, AutomatedCar.Models.AutomatedCar controlledCar) : base(virtualFunctionBus)
        {
            this.controlledCar = controlledCar;
            this.CarPacket = new ControlledCarPacket();
            this.virtualFunctionBus.RegisterComponent(this);
        }

        public override void Process()
        {
            CarPacket.throttleCar = controlledCar.Throttle;
            CarPacket.brakeCar = controlledCar.Brake;
            CarPacket.kmhCar = controlledCar.Speed;
        }
    }
}
