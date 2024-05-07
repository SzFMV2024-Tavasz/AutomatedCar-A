using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Media;
using AutomatedCar.Models;
using AutomatedCar.SystemComponents.Packets.Collision;

namespace AutomatedCar.SystemComponents.Sensors
{
    public class Collision : AbstractSensor
    {
        public Collision(VirtualFunctionBus virtualFunctionBus)
            : base(virtualFunctionBus, 0, 0)
        {
            this.sensorPacket = new CollisionPacket();
            this.virtualFunctionBus.CollisionPacket = (IReadOnlyCollisionPacket)this.sensorPacket;
            //virtualFunctionBus.RegisterComponent(this);
            Console.WriteLine("Collision is on!");
        }

        public override void Process()
        {
            this.CalculateSensorData(World.Instance.ControlledCar, World.Instance.WorldObjects);
            ((CollisionPacket)this.sensorPacket).IsCollided = DetectCollision();
            Console.WriteLine(((CollisionPacket)this.sensorPacket).IsCollided);
        }

        public bool DetectCollision()
        {
            var collidableObjects = World.Instance.WorldObjects.Where(x => x != World.Instance.ControlledCar
            && (x.Collideable || x.WorldObjectType == WorldObjectType.Other)).ToList();

            PolylineGeometry newCarGeometry = this.ActualizeGeometry(World.Instance.ControlledCar.Geometry, World.Instance.ControlledCar);

            foreach (var obj in collidableObjects)
            {
                if (IntersectsWithObject(newCarGeometry, obj))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IntersectsWithObject(PolylineGeometry updatedGeometry, WorldObject obj)
        {
            foreach (var geom in obj.Geometries)
            {
                foreach (var item in geom.Points)
                {
                    Point updatedPoint = GetTransformedPoint(item, obj);

                    if (updatedGeometry.FillContains(updatedPoint))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private PolylineGeometry ActualizeGeometry(PolylineGeometry oldGeom, WorldObject obj)
        {
            List<Point> updatedPoints = new List<Point>();

            foreach (var item in oldGeom.Points)
            {
                Point updatedPoint = GetTransformedPoint(item, obj);

                updatedPoints.Add(updatedPoint);
            }

            return new PolylineGeometry(updatedPoints, false);
        }

        private static Point GetTransformedPoint(Point geomPoint, WorldObject obj)
        {
            double angleInRad = DegToRad(obj.Rotation);

            Point transformedPoint;

            if (!obj.RotationPoint.IsEmpty)
            {
                // offset with the rotationPoint coordinate
                Point offsettedPoint = new Point(geomPoint.X - obj.RotationPoint.X, geomPoint.Y - obj.RotationPoint.Y);

                // now apply rotation
                double rotatedX = (offsettedPoint.X * Math.Cos(angleInRad)) - (offsettedPoint.Y * Math.Sin(angleInRad));
                double rotatedY = (offsettedPoint.X * Math.Sin(angleInRad)) + (offsettedPoint.Y * Math.Cos(angleInRad));

                // offset with the actual coordinate
                transformedPoint = new Point(rotatedX + obj.X, rotatedY + obj.Y);
            }
            else
            {
                // offset with the actual coordinate
                transformedPoint = new Point(geomPoint.X + obj.X, geomPoint.Y + obj.Y);
            }

            return transformedPoint;
        }

        private static double DegToRad(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        protected override bool IsRelevant(WorldObject worldObject)
        {
            return true;
        }
    }
}