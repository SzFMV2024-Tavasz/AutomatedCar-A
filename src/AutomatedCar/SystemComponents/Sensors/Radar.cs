using AutomatedCar.Models;
using AutomatedCar.SystemComponents.Packets;
using AutomatedCar.SystemComponents.Packets.Radar;
using Avalonia;
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
            // Get the vector from the car to the world object
            double deltaX = worldObject.X - World.Instance.ControlledCar.X;
            double deltaY = worldObject.Y - World.Instance.ControlledCar.Y;

            // Calculate the angle between the car's direction vector and the vector to the world object
            double angleToCar = Math.Atan2(deltaY, deltaX) * (180 / Math.PI);
            double angleDifference = Math.Abs(angleToCar - World.Instance.ControlledCar.Rotation);
            angleDifference = angleDifference > 180 ? 360 - angleDifference : angleDifference;

            // Get the angle difference between the car's direction and the object's rotation
            double rotationDifference = Math.Abs(angleToCar - worldObject.Rotation);
            rotationDifference = rotationDifference > 180 ? 360 - rotationDifference : rotationDifference;

            // Check if both angle differences are within a certain range (e.g., 45 degrees)
            if (angleDifference <= 90 && rotationDifference <= 90)
            {
                Console.WriteLine(worldObject.Filename);
                return true; // Object is heading towards the car
            }
            else
            {
                return false; // Object is not heading towards the car
            }
        }
    }
}