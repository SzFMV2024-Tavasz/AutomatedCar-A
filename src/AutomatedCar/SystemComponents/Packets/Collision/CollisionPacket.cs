using ReactiveUI;

namespace AutomatedCar.SystemComponents.Packets.Collision
{
    public class CollisionPacket : AbstractSensorPacket, IReadOnlyCollisionPacket
    {
        private bool isCollided;

        public bool IsCollided
        {
            get => this.isCollided;
            set => this.RaiseAndSetIfChanged(ref this.isCollided, value);
        }
    }
}