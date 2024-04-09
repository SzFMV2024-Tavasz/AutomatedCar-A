using AutomatedCar.SystemComponents.Packets;
using System.Collections.Generic;

namespace AutomatedCar.SystemComponents
{
    public class VirtualFunctionBus : GameBase
    {
        private List<SystemComponent> components = new List<SystemComponent>();

        public IReadOnlyDummyPacket DummyPacket { get; set; }
        public IReadOnlyControlledCarPacket ControlledCarPacket { get; set; }
        public IReadOnlyCollisionPacket CollisionPacket { get; set; }

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