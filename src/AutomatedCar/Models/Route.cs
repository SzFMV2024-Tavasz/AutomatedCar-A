namespace AutomatedCar.Models
{
    using AvaloniaEdit.Rendering;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Route : IRoute
    {
        // File name of the NPC car's .png
        private string objectFileName;

        [JsonProperty("objectFileName")]
        public string ObjectFileName
        {
            get { return objectFileName; }
            set { objectFileName = value; }
        }


        // after getting to the end of the array, going back to the first RoutePoint or not.
        bool repeatAfterFinish;

        [JsonProperty("repeatAfterFinish")]
        public bool RepeatAfterFinish { get { return repeatAfterFinish; } }


        // straightforward
        private bool isPedestrian;

        [JsonProperty("isPedestrian")]
        public bool IsPedestrian
        {
            get { return isPedestrian; }
            set { isPedestrian = value; }
        }

        // straightforward
        private List<RoutePoint> routePoints;

        [JsonProperty("routePoints")]
        public List<RoutePoint> RoutePoints
        {
            get { return routePoints; }
            set { routePoints = value; }
        }

        // The index of the preffered index to be first.
        private int startPointID;

        [JsonProperty("startPointID")]
        public int StartPointID
        {
            get { return startPointID; }
        }

        private int currentPointID;
        [JsonIgnore]
        public int CurrentPointID
        {
            get { return currentPointID; }
            set { currentPointID = value; }
        }


        [JsonConstructor]
        public Route(bool repeatAfterFinish, bool isPedestrian, int startPointID, string objectfileName, List<RoutePoint> routePoints)
        {
            this.repeatAfterFinish = repeatAfterFinish;
            this.isPedestrian = isPedestrian;
            this.routePoints = routePoints;
            this.startPointID = startPointID;
            this.ObjectFileName = objectfileName;
        }

        







    }
}
