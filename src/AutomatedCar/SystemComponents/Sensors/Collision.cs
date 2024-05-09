using System.Collections.Generic;
using AutomatedCar.Models;
using AutomatedCar.SystemComponents.Packets.Collision;
using System.Linq;
using System;
using Avalonia.Media;
using Avalonia;

namespace AutomatedCar.SystemComponents.Sensors
{
    public class Collision : SystemComponent
    {
        private CollisionPacket packet;
        private IEnumerable<WorldObject> collidableWorldObjects;

        public Collision(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus)
        {
            this.packet = new CollisionPacket();
            virtualFunctionBus.CollisionPacket = packet;
            this.collidableWorldObjects = World.Instance.WorldObjects
                .Where(
                        obj => obj.Collideable
                        && obj != World.Instance.ControlledCar
                        && (obj.WorldObjectType == WorldObjectType.Tree
                        // || obj.WorldObjectType == WorldObjectType.Car
                        || obj.WorldObjectType == WorldObjectType.RoadSign)
                        // || obj.WorldObjectType == WorldObjectType.Pedestrian)
                );
            Console.WriteLine("Collison is on!");
        }

        public override void Process()
        {
            this.packet.IsCollided = false;
            var pointOfCar = GetCarPoints(World.Instance.ControlledCar);
            foreach (WorldObject item in this.collidableWorldObjects)
            {
                Console.WriteLine(item.Geometries.Count);
                foreach (Point point in item.Geometries[0].Points)
                {
                    if (pointOfCar.FillContains(new Point(point.X + item.X, point.Y + item.Y)))
                    {
                        this.packet.IsCollided = true;
                    }
                }
            }
            Console.WriteLine(this.packet.IsCollided);
        }

        public static PolylineGeometry GetCarPoints(AutomatedCar.Models.AutomatedCar car)
        {
            var carpoints = ((PolylineGeometry)car.Geometry).Points;

            Point[] points = new Point[((PolylineGeometry)car.Geometry).Points.Count];

            for (int i = 0; i < carpoints.Count; i++)
            {
                points[i] = new Point(Math.Abs((int)carpoints[i].X + car.X), Math.Abs((int)carpoints[i].Y + car.Y));
            }

            return new PolylineGeometry(points, true);
        }
    }
}