namespace AutomatedCar.Models
{
    //using AutomatedCar.SystemComponents;
    using global::AutomatedCar.SystemComponents;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UserControlledCar : AutomatedCar
    {
        public Engine Engine { get; set; }
        public UserControlledCar(int x, int y, string filename) : base(x, y, filename)
        {
             this.Engine = new Engine(VirtualFunctionBus,this);   

        }
        
    }
}
