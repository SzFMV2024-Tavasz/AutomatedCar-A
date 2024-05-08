namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using AutomatedCar.SystemComponents.Packets.Radar;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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
                 ATPacket.CarInFront =  CarInFront();
                if (ATPacket.CarInFront != null)
                {
                    ATPacket.CurrentDistance = DistanceBetweenCars();
                }
                
            }
        }

            
        public NPCCar CarInFront()
        {
            NPCCar temp = null;
            if (World.Instance.ControlledCar.VirtualFunctionBus.RadarPacket.DetectedObjects != null)
            {
                var relevantObjects = World.Instance.ControlledCar.VirtualFunctionBus.RadarPacket.DetectedObjects;
                foreach (var rObj in relevantObjects)
                {
                    if (rObj is NPCCar)
                    {
                        if (Math.Abs(((rObj.Rotation) - World.Instance.ControlledCar.Rotation)) <= 90)
                        {
                            temp = (NPCCar)rObj;
                        }
                        

                    }
                    
                }
            }

            return temp;


        }

        public double DistanceBetweenCars()
        {
            // 1m = 50 px 
            // CTI = cél távolsági idő
            // 

            //ATPacket.CarInFront.Speed;
            //ATPacket.CarInFront.X;
            //ATPacket.CarInFront.Y;

            //World.Instance.ControlledCar.X;
            //World.Instance.ControlledCar.Y;
            //World.Instance.ControlledCar.Speed;


            double DistanceX = Math.Abs(World.Instance.ControlledCar.X - ATPacket.CarInFront.X);
            double DistanceY = Math.Abs(World.Instance.ControlledCar.Y - ATPacket.CarInFront.Y);

            double Distance = Math.Sqrt((DistanceX * DistanceX) + (DistanceY * DistanceY));

            //kettő közti távolság osztva a követő sebességével.

            double DistanceInTime = Distance / Math.Abs(ATPacket.CarInFront.Speed - World.Instance.ControlledCar.Speed);

            return DistanceInTime;

        }


    }
}
