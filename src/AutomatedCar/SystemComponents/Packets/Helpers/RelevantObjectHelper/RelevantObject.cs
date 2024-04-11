using AutomatedCar.Models;

namespace AutomatedCar.SystemComponents.Packets.Helpers.RelevantObjectHelper
{
    public sealed class RelevantObject : IRelevantObject
    {
        private WorldObject relevantObject;
        private double currentDistance;
        private double previousDistance;

        public RelevantObject(WorldObject relevantObject, double currentDistance, double previousDistance)
        {
            this.relevantObject = relevantObject;
            this.currentDistance = currentDistance;
            this.previousDistance = previousDistance;
        }

        public double GetCurrentDistance()
        {
            return this.currentDistance;
        }

        public double GetPreviousDistance()
        {
            return this.previousDistance;
        }

        public WorldObject GetRelevantObject()
        {
            return this.relevantObject;
        }

        public void SetCurrentDistance(double distance)
        {
            this.currentDistance = distance;
        }

        public void SetPreviousDistance(double distance)
        {
            this.previousDistance = distance;
        }
    }
}