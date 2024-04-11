using AutomatedCar.Models;
using ReactiveUI;
using System.Collections.Generic;

namespace AutomatedCar.SystemComponents.Packets.Camera
{
    public sealed class CameraPacket : AbstractSensorPacket, IReadOnlyCameraPacket
    {
        private List<WorldObject> roads;

        public CameraPacket()
        {
            this.Roads = new List<WorldObject>();
        }

        public List<WorldObject> Roads
        {
            get => this.roads;
            set => this.RaiseAndSetIfChanged(ref this.roads, value);
        }
    }
}