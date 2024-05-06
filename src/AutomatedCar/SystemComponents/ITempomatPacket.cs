namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITempomatPacket
    {
        bool IsItOn { get; set; }

        int WantedSpeed { get; set; }
        double WantedDistance { get; set; }

    }
}
