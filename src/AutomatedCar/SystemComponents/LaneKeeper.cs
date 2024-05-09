namespace AutomatedCar.SystemComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutomatedCar.Models;
    using System.Threading.Tasks;
    using AutomatedCar.SystemComponents.Packets;
    using AutomatedCar.Helpers;
    using Avalonia;
    using Avalonia.Media;

    public class LaneKeeper : SystemComponent
    {
        private VirtualFunctionBus virtualFunctionBus;

        private LaneKeeperPacket LaneKeeperPacket { get; set; }

        public LaneKeeper(VirtualFunctionBus virtualFunctionBus) : base(virtualFunctionBus)
        {
            this.virtualFunctionBus = virtualFunctionBus;
            this.LaneKeeperPacket = new LaneKeeperPacket();
            virtualFunctionBus.RegisterComponent(this);
        }

        private void CheckLane(AutomatedCar automatedCar)
        {
            if (true)
            {
                var carCenterRotationPoint = new Point(automatedCar.RotationPoint.X, automatedCar.RotationPoint.Y);
                var extended = new Point(automatedCar.X, automatedCar.Y);
                var carCenterPoint = new Point(automatedCar.X, automatedCar.Y);
                var extendedRotatedFromCar = RotatePoint(extended, carCenterPoint, automatedCar.Rotation);
                int distancetreshold = 150;
                var allRoads = World.Instance.WorldObjects.Where(obj => obj.WorldObjectType  == WorldObjectType.Road);
                var relevantRoads = allRoads.Where(obj => obj.Filename.Contains("45") || obj.Filename.Contains("6") || obj.Filename.Contains("straight"));
                WorldObject closestRoad = relevantRoads.FirstOrDefault();
                double closesDistance = Utils.DistanceBetween(this.FindClosestLanePoint(closestRoad, extendedRotatedFromCar), extendedRotatedFromCar);
                foreach (var road in relevantRoads )
                {
                    var closestPoint = this.FindClosestLanePoint(road, extendedRotatedFromCar);
                    double distance = Utils.DistanceBetween(closestPoint, extendedRotatedFromCar);
                    if (distance < closesDistance)
                    {
                        closestRoad = road;
                        closesDistance = distance;
                    }
                }

                if (closestRoad != null && closesDistance < distancetreshold)
                {
                    this.LaneKeeperPacket.IsLaneKeepingPossible = true;
                    var closestPoint = this.FindClosestLanePoint(closestRoad, extendedRotatedFromCar);
                    double rotationAbs = automatedCar.Rotation;
                    if (automatedCar.Rotation < 0)
                    {
                        rotationAbs = 360 - Math.Abs(automatedCar.Rotation);
                    }

                    if (automatedCar.IsLaneKeeperOn)
                    {
                        if (closestPoint != null)
                                            {
                                                // ^
                                                if (rotationAbs >= 315 || rotationAbs < 45)
                                                {
                                                    if(automatedCar.X > closestPoint.X)
                                                    {
                                                        OnLeftEvent(automatedCar);
                                                    }
                                                    else
                                                    {
                                                        OnRightEvent(automatedCar);
                                                    }
                                                }

                                                // >
                                                if (rotationAbs >= 45 && rotationAbs < 135)
                                                {
                                                    if (automatedCar.Y > closestPoint.Y)
                                                    {
                                                        OnLeftEvent(automatedCar);
                                                    }
                                                    else
                                                    {
                                                        OnRightEvent(automatedCar);
                                                    }
                                                }

                                                // v
                                                if (rotationAbs >= 135 && rotationAbs < 225)
                                                {
                                                    if (automatedCar.X < closestPoint.X)
                                                    {
                                                        OnLeftEvent(automatedCar);
                                                    }
                                                    else
                                                    {
                                                        OnRightEvent(automatedCar);
                                                    }
                                                }

                                                // <
                                                if (rotationAbs >= 225 && rotationAbs < 315)
                                                {
                                                    if (automatedCar.Y < closestPoint.Y)
                                                    {
                                                        OnLeftEvent(automatedCar);
                                                    }
                                                    else
                                                    {
                                                        OnRightEvent(automatedCar);
                                                    }
                                                }
                                            }
                    }
                }
                else
                {
                    this.LaneKeeperPacket.IsLaneKeepingPossible = false;
                    
                }
            }
        }

        static Point RotatePoint(Point pointToRotate, Point centerPoint, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            var x = (int)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X);
            var y = (int)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y);
            return new Point(x, y);
        }

        private Point FindClosestLanePoint(WorldObject road, Point carCenter)
        {
            var laneGeometry = road.Geometries[1];
            if (laneGeometry != null)
            {
                
                var roadPoint = new Point(road.X, road.Y);
                PolylineGeometry absolute = new PolylineGeometry();
                foreach (Point p in laneGeometry.Points)
                {
                    Point newpoint = new Point(p.X + road.X, p.Y + road.Y);
                    //Point rotated = RotatePoint(newpoint, roadPoint, road.Rotation);
                    absolute.Points.Add(newpoint);
                }
                return absolute.Points.OrderBy(p => Utils.DistanceBetween(p, carCenter)).FirstOrDefault();
            }

            return carCenter;
        }

        protected virtual void OnLeftEvent(AutomatedCar automatedCar)
        {
            automatedCar.SteeringWheelRotation = -1;
            automatedCar.MovementTurnLeft();
            LaneKeeperPacket.Debug = "left";
        }

        protected virtual void OnRightEvent(AutomatedCar automatedCar)
        {
            automatedCar.SteeringWheelRotation = 1;
            automatedCar.MovementTurnRight();
            LaneKeeperPacket.Debug = "right";
        }

        public void ToggleLaneKeeper()
        {
            World.Instance.ControlledCar.IsLaneKeeperOn = !World.Instance.ControlledCar.IsLaneKeeperOn;
        }


        public override void Process()
        {
            CheckLane(World.Instance.ControlledCar);
            this.LaneKeeperPacket.IsLaneKeeperOn = World.Instance.ControlledCar.IsLaneKeeperOn;
        }
    }
}
