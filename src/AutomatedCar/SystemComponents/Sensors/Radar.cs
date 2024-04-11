using AutomatedCar.Models;
using AutomatedCar.SystemComponents.Packets;
using AutomatedCar.SystemComponents.Packets.Radar;
using System;

namespace AutomatedCar.SystemComponents.Sensors
{
    public sealed class Radar : AbstractSensor
    {
        public Radar(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus, 60, 200)
        {
            this.sensorPacket = new RadarPacket();
            this.virtualFunctionBus.RadarPacket = (IReadOnlySensorPacket)this.sensorPacket;
            virtualFunctionBus.RegisterComponent(this);
            Console.WriteLine("Radar is on!");
        }

        public override void Process()
        {
            this.CalculateSensorData(World.Instance.ControlledCar, World.Instance.WorldObjects);
        }

        protected override bool IsRelevant(WorldObject worldObject)
        {
            return true;
        }
    }
}