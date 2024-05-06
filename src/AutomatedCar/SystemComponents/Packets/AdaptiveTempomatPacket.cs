namespace AutomatedCar.SystemComponents.Packets
{
    using AutomatedCar.Models;
    using ReactiveUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

     class AdaptiveTempomatPacket : ReactiveObject
    {
        private int wantedspeed;

        public int WantedSpeed
        {
            get { return wantedspeed; }
            set { this.RaiseAndSetIfChanged(ref this.wantedspeed, value); }
        }

        private double wantedDistance;

        public double WantedDistance
        {
            get { return wantedDistance; }
            set { this.RaiseAndSetIfChanged(ref this.wantedDistance, value); }
        }

        private double currentDistance;

        public double CurrentDistance
        {
            get { return currentDistance; }
            set { this.RaiseAndSetIfChanged(ref this.currentDistance, value); }
        }

        private NPCCar carInFront;

        public NPCCar CarInFront
        {
            get { return carInFront; }
            set { this.RaiseAndSetIfChanged(ref this.carInFront, value); }
        }

        private int speedLimit;

        public int SpeedLimit
        {
            get { return speedLimit; }
            set { this.RaiseAndSetIfChanged(ref this.speedLimit, value); ; }
        }
    }
}
