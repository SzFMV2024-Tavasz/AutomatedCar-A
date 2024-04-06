﻿namespace AutomatedCar.SystemComponents.Packets
{
    using AutomatedCar.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISensorPacket
    {
        IList<WorldObject> DetectedObjects { get; set; }

        IList<WorldObject> RelevantObjects { get; set; }

        WorldObject ClosestObject { get; set; }
    }
}
