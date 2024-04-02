namespace AutomatedCar.SystemComponents
{
    using AutomatedCar.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class NPCSpeedComponent : SystemComponent
    {
        NPCCar car;
        public NPCSpeedComponent(VirtualFunctionBus virtualFunctionBus,NPCCar car) : base(virtualFunctionBus)
        {
            this.car = car;
        }

        public override void Process()
        {
            
        }
    }
}
