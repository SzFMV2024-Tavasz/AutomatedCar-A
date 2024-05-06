using AutomatedCar.SystemComponents.Packets;
using AutomatedCar.SystemComponents.Packets.Camera;
using AutomatedCar.SystemComponents.Packets.Collision;
using System.Collections.Generic;

namespace AutomatedCar.SystemComponents
{
    public class VirtualFunctionBus : GameBase
    {
        private List<SystemComponent> components = new List<SystemComponent>();

        public IReadOnlyCollisionPacket CollisionPacket { get; set; }

        public IReadOnlyCameraPacket CameraPacket { get; set; }

        public IReadOnlySensorPacket RadarPacket { get; set; }

        public IReadOnlyControlledCarPacket ControlledCarPacket { get; set; }

        public ITempomatPacket TempomatPacket {get; set;}

        public void RegisterComponent(SystemComponent component)
        {
            this.components.Add(component);
        }

        protected override void Tick()
        {
            foreach (SystemComponent component in this.components)
            {
                component.Process();
            }
        }
    }
}