namespace AutomatedCar.SystemComponents.Packets
{
    using AutomatedCar.Models;
    using ReactiveUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class CameraPacket : SensorPacket, ICameraPacket
    {
        private IList<WorldObject> roads;

        public CameraPacket()
        {
            this.Roads = new List<WorldObject>();
        }

        public IList<WorldObject> Roads
        {
            get => this.roads;
            set => this.RaiseAndSetIfChanged(ref this.roads, value);
        }
    }
}
