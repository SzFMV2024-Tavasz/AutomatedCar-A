namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class LaneKeeperPacket : ReactiveObject, ILaneKeeperPacket
    {
        private bool isLaneKeeperOn;
        private bool isLaneKeepingPossible;
        private string debug;

        public LaneKeeperPacket()
        {
            this.isLaneKeeperOn = false;
            this.isLaneKeepingPossible = false;
            this.debug = "";
        }

        public bool IsLaneKeeperOn
        {
            get => this.isLaneKeeperOn;
            set => this.RaiseAndSetIfChanged(ref this.isLaneKeeperOn, value);
        }

        public bool IsLaneKeepingPossible
        {
            get => this.isLaneKeepingPossible;
            set => this.RaiseAndSetIfChanged(ref this.isLaneKeepingPossible, value);
        }

        

        public string Debug
        {
            get => this.debug;
            set => this.RaiseAndSetIfChanged(ref this.debug, value);
        }
    }
}
