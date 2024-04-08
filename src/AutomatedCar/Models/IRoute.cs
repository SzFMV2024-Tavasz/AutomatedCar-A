namespace AutomatedCar.Models
{
    using System.Collections.Generic;

    public interface IRoute
    {
        int CurrentPointID { get; set; }
        bool IsPedestrian { get; set; }
        string ObjectFileName { get; set; }
        bool RepeatAfterFinish { get; }
        List<RoutePoint> RoutePoints { get; set; }
        int StartPointID { get; }
    }
}