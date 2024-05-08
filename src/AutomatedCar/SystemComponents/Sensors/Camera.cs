using System;
using System.Collections.Generic;
using AutomatedCar.Models;
using AutomatedCar.SystemComponents.Packets.Camera;
using AutomatedCar.SystemComponents.Packets.Helpers.RelevantObjectHelper;

namespace AutomatedCar.SystemComponents.Sensors
{
    public sealed class Camera : AbstractSensor
    {
        public Camera(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus, 60, 80)
        {
            this.sensorPacket = new CameraPacket();
            virtualFunctionBus.CameraPacket = (IReadOnlyCameraPacket)this.sensorPacket;
            //virtualFunctionBus.RegisterComponent(this);
            Console.WriteLine("Camera is on!");
        }

        public override void Process()
        {
            this.CalculateSensorData(World.Instance.ControlledCar, World.Instance.WorldObjects);
            this.FilterRoads();
        }

        protected override bool IsRelevant(WorldObject worldObject)
        {
            return worldObject.WorldObjectType == WorldObjectType.Road || worldObject.WorldObjectType == WorldObjectType.RoadSign;
        }

        private void FilterRoads()
        {
            List<WorldObject> roads = new List<WorldObject>();
            foreach (IRelevantObject ro in this.sensorPacket.RelevantObjects)
            {
                if (ro.GetRelevantObject().WorldObjectType == WorldObjectType.Road)
                {
                    roads.Add(ro.GetRelevantObject());
                }
            }

            ((CameraPacket)this.sensorPacket).Roads = roads;
            
        }
    }
}