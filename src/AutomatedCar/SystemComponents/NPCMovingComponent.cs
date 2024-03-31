namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class NPCMovingComponent : SystemComponent
    {
        NPCCar car; 

        public NPCMovingComponent(VirtualFunctionBus virtualFunctionBus, NPCCar car) 
            : base(virtualFunctionBus)
        {
            this.car = car;
        }

        double leftX;
        double leftY;

        public override void Process()
        {
            double plusX = (double)car.Speed / GameBase.TicksPerSecond;
            leftX += plusX - Math.Floor(plusX);

            if (leftX > 1)
            {
                car.X += (int)leftX + (int)plusX;
                leftX = leftX - Math.Floor(leftX);
            }
            else
            {
                car.X += (int)plusX;
            }
            
        }

    }
}
