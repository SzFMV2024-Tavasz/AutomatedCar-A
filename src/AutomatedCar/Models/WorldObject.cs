namespace AutomatedCar.Models
{
    using Avalonia.Media;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing;

    public class PropertyChangedEventArgs : EventArgs
    {
        public PropertyChangedEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; set; }
    }

    public class WorldObject
    {
        public event EventHandler<PropertyChangedEventArgs> PropertyChangedEvent;

        private int x;
        private int y;
        private double throttle;
        private double brake;


        private double rotation;

        public WorldObject(int x, int y, string filename, int zindex = 1, bool collideable = false, WorldObjectType worldObjectType = WorldObjectType.Other)
        {
            this.X = x;
            this.Y = y;
            this.Filename = filename;
            this.ZIndex = zindex;
            this.Collideable = collideable;
            this.WorldObjectType = worldObjectType;
            //this.Throttle = throttle;
        }

        public int ZIndex { get; set; }

        public double Rotation
        {
            get => this.rotation;
            set
            {
                this.rotation = value % 360;
                this.PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Rotation)));
            }
        }

        public int X
        {
            get => this.x;
            set
            {
                this.x = value;
                this.PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs(nameof(this.X)));
            }
        }

        public int Y
        {
            get => this.y;
            set
            {
                this.y = value;
                this.PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Y)));
            }
        }

        public double Throttle
        {
            get => this.throttle;
            set
            {
                this.throttle = value;
                this.PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Throttle)));
            }
        }

        public double Brake
        {
            get => this.brake;
            set
            {
                this.brake = value;
                this.PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Brake)));
            }
        }

        public Point RotationPoint { get; set; }

        public string RenderTransformOrigin { get; set; }

        public List<PolylineGeometry> Geometries { get; set; } = new ();

        public List<PolylineGeometry> RawGeometries { get; set; } = new ();

        public string Filename { get; set; }

        public bool Collideable { get; set; }

        public WorldObjectType WorldObjectType { get; set; }

        
    }
}