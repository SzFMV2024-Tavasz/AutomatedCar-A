using AutomatedCar.Models;
using System.Collections.Generic;

namespace AutomatedCar.SystemComponents.Packets.Camera
{
    public interface IReadOnlyCameraPacket
    {
        public List<WorldObject> Roads { get; }
    }
}