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
        private AutomatedCar automatedCar;

        private LaneKeeperPacket LaneKeeperPacket { get; set; }

        public LaneKeeper(VirtualFunctionBus virtualFunctionBus, AutomatedCar automatedCar) : base(virtualFunctionBus)
        {
            this.virtualFunctionBus = virtualFunctionBus;
            this.LaneKeeperPacket = new LaneKeeperPacket();
            virtualFunctionBus.RegisterComponent(this);
            this.automatedCar = automatedCar;
        }

        private void CheckLane()
        {
            if (this.automatedCar.CarTransmission == AutomatedCar.Transmissions.D || this.automatedCar.CarTransmission == AutomatedCar.Transmissions.R)
            {
                var carCenterRotationPoint = new Point(this.automatedCar.X, this.automatedCar.Y);
                var extended = new Point(this.automatedCar.X + 50, this.automatedCar.Y + 50);
                var carCenter = Utils.RotatePoint(extended, this.automatedCar.RotationPoint, this.automatedCar.Rotation);
                int distancetreshold = 50;
                var Roads = World.Instance.WorldObjects.Where(obj => obj.WorldObjectType  == WorldObjectType.Road);
                var niceRoads = Roads.Where(obj => obj.Filename.Contains("45") || obj.Filename.Contains("6") || obj.Filename.Contains("straight"));
                WorldObject closestRoad = niceRoads.FirstOrDefault();
                double closesDistance = Utils.DistanceBetween(FindClosestLanePoint(closestRoad, carCenter), carCenter);
                foreach (var road in niceRoads )
                {
                    var closestPoint = FindClosestLanePoint(road, carCenter);
                    double distance = Utils.DistanceBetween(closestPoint, carCenter);
                    if (distance < closesDistance)
                    {
                        closestRoad = road;
                        closesDistance = distance;
                    }
                }

                if (closestRoad != null && closesDistance<100)
                {
                    var closestPoint = FindClosestLanePoint(closestRoad, carCenter);
                    double rotationAbs = this.automatedCar.Rotation;
                    if (this.automatedCar.Rotation < 0)
                    {
                        rotationAbs = 360 + this.automatedCar.Rotation;
                    }


                    if (closestPoint != null)
                    {
                        // ^
                        if (rotationAbs >= 315 || rotationAbs < 45)
                        {
                            if(this.automatedCar.Y > closestPoint.Y)
                            {
                                OnLeftEvent();
                            }
                            else
                            {
                                OnRightEvent();
                            }
                        }

                        // >
                        if (rotationAbs >= 45 && rotationAbs < 135)
                        {
                            if (this.automatedCar.X > closestPoint.X)
                            {
                                OnLeftEvent();
                            }
                            else
                            {
                                OnRightEvent();
                            }
                        }

                        // v
                        if (rotationAbs >= 135 && rotationAbs < 225)
                        {
                            if (this.automatedCar.Y < closestPoint.Y)
                            {
                                OnLeftEvent();
                            }
                            else
                            {
                                OnRightEvent();
                            }
                        }

                        // <
                        if (rotationAbs >= 225 && rotationAbs < 315)
                        {
                            if (this.automatedCar.X < closestPoint.X)
                            {
                                OnLeftEvent();
                            }
                            else
                            {
                                OnRightEvent();
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
                    absolute.Points.Add(newpoint);
                }

                

                return absolute.Points.OrderBy(p => Utils.DistanceBetween(p, carCenter)).FirstOrDefault();
            }

            return carCenter;
        }

        protected virtual void OnLeftEvent()
        {
            automatedCar.SteeringWheelRotation = -2;
            automatedCar.MovementTurnLeft();
            LaneKeeperPacket.Debug = "left";
        }

        protected virtual void OnRightEvent()
        {
            automatedCar.SteeringWheelRotation = 2;
            automatedCar.MovementTurnRight();
            LaneKeeperPacket.Debug = "right";
        }


        public override void Process()
        {
            CheckLane();
        }
    }
}
