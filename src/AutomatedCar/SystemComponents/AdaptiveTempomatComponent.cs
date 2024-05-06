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


       


        private bool isItOn;
        public bool IsItOn { get { return isItOn; } set
            { 
                isItOn = value;
            } }


        public AdaptiveTempomatComponent(VirtualFunctionBus virtualFunctionBus) : base(virtualFunctionBus)
        {
            CarInFront = null;
            isItOn = false;
            virtualFunctionBus.TempomatPacket = this;
            
        }

        public override void Process()
        {
            
        }

    }
}
