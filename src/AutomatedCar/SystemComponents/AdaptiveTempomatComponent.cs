namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents.Packets;
    using AutomatedCar.SystemComponents.Packets.Helpers.RelevantObjectHelper;
    using AutomatedCar.SystemComponents.Packets.Radar;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            ATPacket.WantedDistance = 0.8;
            
            
        }

        public override void Process()
        {
            if (World.Instance.ControlledCar.IsEmergencyBreakOn)
            {
                ATPacket.IsItOn = false;
            }
            if (ATPacket.IsItOn)
            {
                 ATPacket.CarInFront =  CarInFront();
                WorldObjectListCheck();
                if (ATPacket.CarInFront != null)
                {
                    ATPacket.CurrentDistance = DistanceBetweenCars();
                    if (ATPacket.WantedSpeed < Math.Round(World.Instance.ControlledCar.Velocity))
                    {
                        World.Instance.ControlledCar.Deccelerte();
                        return;
                    }
                    if (Math.Round(ATPacket.CurrentDistance,1)  < ATPacket.WantedDistance)
                    {
                        World.Instance.ControlledCar.Deccelerte();
                        return;
                    }
                    if (Math.Round(ATPacket.CurrentDistance, 1) > ATPacket.WantedDistance && ATPacket.WantedSpeed > Math.Round(World.Instance.ControlledCar.Velocity))
                    {
                        World.Instance.ControlledCar.Accelerate();
                        return;
                    }

                }
                else
                {
                    if (Math.Round(World.Instance.ControlledCar.Velocity) < ATPacket.WantedSpeed)
                    {
                        World.Instance.ControlledCar.Accelerate();
                    }
                    else if (Math.Round(World.Instance.ControlledCar.Velocity) > ATPacket.WantedSpeed)
                    {
                        World.Instance.ControlledCar.Deccelerte();
                    }

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
                    if (ATPacket.SpeedLimit != limit)
                    {
                        ATPacket.SpeedLimit = limit;
                        if (ATPacket.WantedSpeed > limit)
                        {
                            ATPacket.WantedSpeed = limit;
                        }
                        
                    }
                    //ATPacket.WantedSpeed = limit;
                }
            }

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
