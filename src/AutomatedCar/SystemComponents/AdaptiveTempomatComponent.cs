namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class AdaptiveTempomatComponent : SystemComponent
    {
        private int wantedspeed;

        public int WantedSpeed
        {
            get { return wantedspeed; }
            set { wantedspeed = value; }
        }

        private double wantedDistance;

        public double WantedDistance
        {
            get { return wantedDistance; }
            set { wantedDistance = value; }
        }

        private double currentDistance;

        public double CurrentDistance
        {
            get { return currentDistance; }
            set { currentDistance = value; }
        }

        private NPCCar carInFront;

        public NPCCar CarInFront
        {
            get { return carInFront; }
            set { carInFront = value; }
        }
        
        private int speedLimit;

        public int SpeedLimit
        {
            get { return speedLimit; }
            set { speedLimit = value; }
        }

        public AdaptiveTempomatComponent(VirtualFunctionBus virtualFunctionBus) : base(virtualFunctionBus)
        {
            CarInFront = null;
        }

        public override void Process()
        {
            
        }
    }
}
