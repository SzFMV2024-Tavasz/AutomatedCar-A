﻿namespace AutomatedCar.SystemComponents.Packets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using System.Dynamic;

    public interface ILaneKeeperPacket
    {
        bool IsLaneKeeperOn { get; }
        bool IsLaneKeepingPossible { get; }
        string Debug { get; }
    }
}
