﻿namespace AutomatedCar.SystemComponents.Sensors
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class Camera : Sensor
    {
        public Camera(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus, 60, 80)
        {
            this.sensorPacket = new CameraPacket();
            virtualFunctionBus.CameraPacket = (ICameraPacket)this.sensorPacket;
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
            ((CameraPacket)this.sensorPacket).Roads =
                this.sensorPacket
                .RelevantObjects
                .Where(ro => ro.WorldObjectType == WorldObjectType.Road)
                .ToList();
        }
    }
}
