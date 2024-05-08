namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class AdaptiveTempomatComponent : SystemComponent
    {



        AdaptiveTempomatPacket ATPacket;


        public AdaptiveTempomatComponent(VirtualFunctionBus virtualFunctionBus) : base(virtualFunctionBus)
        {
            
            ATPacket = new AdaptiveTempomatPacket();
            virtualFunctionBus.TempomatPacket = ATPacket;
            ATPacket.CarInFront = null;
            ATPacket.IsItOn = false;
            
            
        }

        public override void Process()
        {
            if (ATPacket.IsItOn)
            {
                if (ATPacket.CarInFront != null)
                {
                    CarInFront();
                }
            }
        }


        public void CarInFront()
        {
            //if (ATPacket.IsItOn)
            //{
            //    if (ATPacket.CarInFront != null)
            //    {
                    if (ATPacket.CarInFront.Speed <= ATPacket.WantedSpeed)
                    {
                        ATPacket.WantedSpeed = (int)ATPacket.CarInFront.Speed;
                    }
                    
            //    }
            //}
        }

    }
}
