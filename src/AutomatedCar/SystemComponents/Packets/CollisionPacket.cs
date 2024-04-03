namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
