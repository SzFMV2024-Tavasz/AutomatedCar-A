using AutomatedCar.Models;

namespace AutomatedCar.SystemComponents.Packets.Helpers.RelevantObjectHelper
{
    public interface IRelevantObject
    {
        double GetCurrentDistance();
        double GetPreviousDistance();
        WorldObject GetRelevantObject();
    }
}