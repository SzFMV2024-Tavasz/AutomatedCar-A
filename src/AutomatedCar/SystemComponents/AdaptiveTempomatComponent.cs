namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using AutomatedCar.SystemComponents.Packets.Radar;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class AdaptiveTempomatComponent : SystemComponent
    {



        AdaptiveTempomatPacket ATPacket;
        RadarPacket RPacket;
        
        public AdaptiveTempomatComponent(VirtualFunctionBus virtualFunctionBus) : base(virtualFunctionBus)
        {
            
            ATPacket = new AdaptiveTempomatPacket();
            virtualFunctionBus.TempomatPacket = ATPacket;
            virtualFunctionBus.RadarPacket = RPacket;
            ATPacket.CarInFront = null;
            ATPacket.IsItOn = false;
            
            
        }

        public override void Process()
        {
            
            if (ATPacket.IsItOn)
            {
                 ATPacket.CarInFront =  CarInFront();
                if (ATPacket.CarInFront != null)
                {

                }
            }
        }

        public NPCCar CarInFront()
        {
            var relevantObjects = RPacket.RelevantObjects;
            if (relevantObjects != null) 
            {
                foreach (var rObj in relevantObjects)
                {
                    if (rObj.GetType() is NPCCar)
                    {
                        if (Math.Abs((((rObj as NPCCar).Rotation)- World.Instance.ControlledCar.Rotation)) <= 20)
                        {
                            return (rObj as NPCCar);
                        }
                        
                    }
                    
                }
            }

        }

        public int DistanceBetweenCars()
        {

        }


    }
}
