using AutomatedCar.Models;
using AutomatedCar.SystemComponents.Packets.Helpers.RelevantObjectHelper;
using System.Collections.Generic;

namespace AutomatedCar.SystemComponents.Packets
{
    public interface IReadOnlySensorPacket
    {
        List<WorldObject> DetectedObjects { get; }
        List<IRelevantObject> RelevantObjects { get; }
        IRelevantObject ClosestObject { get; }
    }
}