using ReactiveUI;

namespace AutomatedCar.SystemComponents.Packets
{
    public class CollisionPacket : ReactiveObject, IReadOnlyCollisionPacket
    {
        private bool isCollided;

        public bool IsCollided
        {
            get => this.isCollided;
            set => this.RaiseAndSetIfChanged(ref this.isCollided, value);
        }
    }
}