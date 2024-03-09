using System.Collections.ObjectModel;
using AutomatedCar.Models;
using System.Linq;

using ReactiveUI;

namespace AutomatedCar.ViewModels
{
    using Avalonia.Controls;
    using Models;
    using System;
    using Visualization;

    public class CourseDisplayViewModel : ViewModelBase
    {
        public ObservableCollection<WorldObjectViewModel> WorldObjects { get; } = new ObservableCollection<WorldObjectViewModel>();
      
        private Avalonia.Vector offset;

        public CourseDisplayViewModel(World world)
        {
            this.WorldObjects = new ObservableCollection<WorldObjectViewModel>(world.WorldObjects.Select(wo => new WorldObjectViewModel(wo)));
            this.Width = world.Width;
            this.Height = world.Height;
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public Avalonia.Vector Offset
        {
            get => this.offset;
            set => this.RaiseAndSetIfChanged(ref this.offset, value);
        }

        private DebugStatus debugStatus = new DebugStatus();

        public DebugStatus DebugStatus
        {
            get => this.debugStatus;
            set => this.RaiseAndSetIfChanged(ref this.debugStatus, value);
        }

        public void KeyUp()
        {
            if (World.Instance.ControlledCar.CanGoUp)
            {
                World.Instance.ControlledCar.Y -= 5;
            }
        }

        public void KeyDown()
        {
            if (World.Instance.ControlledCar.CanGoDown)
            {
                World.Instance.ControlledCar.Y += 5;
            }
        }

        public void KeyLeft()
        {
            World.Instance.ControlledCar.X -= 5;
        }

        public void KeyRight()
        {
            World.Instance.ControlledCar.X += 5;
        }

        public void PageUp()
        {
            if (World.Instance.ControlledCar.CanRotate && World.Instance.ControlledCar.Velocity != 0)
            {
                World.Instance.ControlledCar.Rotation += 5;
            }
        }

        public void PageDown()
        {
            if (World.Instance.ControlledCar.CanRotate && World.Instance.ControlledCar.Velocity != 0)
            {
                World.Instance.ControlledCar.Rotation -= 5;
            }
        }

        public void ToggleDebug()
        {
            this.debugStatus.Enabled = !this.debugStatus.Enabled;
        }

        public void ToggleCamera()
        {
            this.DebugStatus.Camera = !this.DebugStatus.Camera;
        }
        //Transmission  controllers
        public void TransmissionToP()
        {
            if (World.Instance.ControlledCar.Velocity == 0&& World.Instance.ControlledCar.CarTransmission == AutomatedCar.Transmission.R)
            {
                World.Instance.ControlledCar.CanGoUp = false;
                World.Instance.ControlledCar.CanGoDown = false;
                World.Instance.ControlledCar.CanRotate = false;
                World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmission.P;
            }
        }
        public void TransmissionToR()
        {
            if (World.Instance.ControlledCar.Velocity==0&&(World.Instance.ControlledCar.CarTransmission == AutomatedCar.Transmission.P|| World.Instance.ControlledCar.CarTransmission == AutomatedCar.Transmission.N))
            {
                World.Instance.ControlledCar.CanGoDown = true;
                World.Instance.ControlledCar.CanGoUp = false;
                World.Instance.ControlledCar.CanRotate = true;
                World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmission.R;
            }
        }
        public void TransmissionToN()
        {
            if(World.Instance.ControlledCar.CarTransmission==AutomatedCar.Transmission.R|| World.Instance.ControlledCar.CarTransmission == AutomatedCar.Transmission.D)
            World.Instance.ControlledCar.CanGoDown = false;
            World.Instance.ControlledCar.CanGoUp = false;
            World.Instance.ControlledCar.CanRotate = true;
            World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmission.N;
        }
        public void TransmissionToD()
        {
            if(World.Instance.ControlledCar.CarTransmission == AutomatedCar.Transmission.N)
            World.Instance.ControlledCar.CanGoDown = false;
            World.Instance.ControlledCar.CanGoUp = true;
            World.Instance.ControlledCar.CanRotate = true;
            World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmission.D;
        }

        public void ToggleRadar()
        {
            // World.Instance.DebugStatus.Radar = !World.Instance.DebugStatus.Radar;
        }

        public void ToggleUltrasonic()
        {
            //World.Instance.DebugStatus.Ultrasonic = !World.Instance.DebugStatus.Ultrasonic;
        }

        public void ToggleRotation()
        {
            //World.Instance.DebugStatus.Rotate = !World.Instance.DebugStatus.Rotate;
        }

        public void FocusCar(ScrollViewer scrollViewer)
        {
            var offsetX = World.Instance.ControlledCar.X - (scrollViewer.Viewport.Width / 2);
            var offsetY = World.Instance.ControlledCar.Y - (scrollViewer.Viewport.Height / 2);
            this.Offset = new Avalonia.Vector(offsetX, offsetY);
        }
    }
}