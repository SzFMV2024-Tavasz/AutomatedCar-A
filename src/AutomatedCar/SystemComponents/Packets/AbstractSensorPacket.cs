using AutomatedCar.Models;
using AutomatedCar.SystemComponents.Packets.Helpers.RelevantObjectHelper;
using ReactiveUI;
using System.Collections.Generic;

namespace AutomatedCar.SystemComponents.Packets
{
    public abstract class AbstractSensorPacket : ReactiveObject, IReadOnlySensorPacket
    {
        private List<WorldObject> detectedObjects;
        private List<IRelevantObject> relevantObjects;
        private IRelevantObject closestObject;

        public AbstractSensorPacket()
        {
            this.detectedObjects = new List<WorldObject>();
            this.relevantObjects = new List<IRelevantObject>();
        }

        public List<WorldObject> DetectedObjects
        {
            get => this.detectedObjects;
            set => this.RaiseAndSetIfChanged(ref this.detectedObjects, value);
        }

        public List<IRelevantObject> RelevantObjects
        {
            get => this.relevantObjects;
            set => this.RaiseAndSetIfChanged(ref this.relevantObjects, value);
        }

        public IRelevantObject ClosestObject
        {
            get => this.closestObject;
            set => this.RaiseAndSetIfChanged(ref this.closestObject, value);
        }
    }
}