namespace AutomatedCar.SystemComponents.Packets
{
    using AutomatedCar.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IReadonlyCameraPacket : ISensorPacket
    {
        public IList<WorldObject> Roads { get; set; }
    }

}
