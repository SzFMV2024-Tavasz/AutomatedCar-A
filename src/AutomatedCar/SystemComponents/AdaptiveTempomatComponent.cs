namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using AutomatedCar.SystemComponents.Packets.Helpers.RelevantObjectHelper;
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
            
        }


        private void WorldObjectListCheck()
        {
            foreach (var wo in (virtualFunctionBus.CameraPacket as AbstractSensorPacket).RelevantObjects)
            {
                SignCheck(wo.GetRelevantObject());
            }
        }
        private void SignCheck(WorldObject wo)
        {

            if (wo.WorldObjectType == WorldObjectType.RoadSign)
            {
                string signType = (wo.Filename.Split("_")[1]);
                if (signType.Equals("speed"))
                {
                    int limit = int.Parse((wo.Filename.Split("_")[2]).Split(".")[0]);
                    ATPacket.WantedSpeed = limit;
                }
            }

        }

    }
}
