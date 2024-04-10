using System.Collections.ObjectModel;
using AutomatedCar.Models;
using System.Linq;

using ReactiveUI;

namespace AutomatedCar.ViewModels
{
    using Avalonia.Controls;
    using Models;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Threading;
    using System.Threading.Tasks;
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

        //public bool KeyUpPressed { get; set; }

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
            World.Instance.ControlledCar.KeyUpPressed = true;
            if (World.Instance.ControlledCar.CanGoUp)
            {
                 World.Instance.ControlledCar.Accelerate();
                //World.Instance.ControlledCar.MovementForward();
            }
            if (World.Instance.ControlledCar.CanGoDown)
            {
                //Deccelerte();
                World.Instance.ControlledCar.Accelerate();
                //World.Instance.ControlledCar.MovementBackward();
            }
        }

        public void KeyDown()
        {
            World.Instance.ControlledCar.KeyDownPressed = true;
            World.Instance.ControlledCar.Deccelerte();
            World.Instance.ControlledCar.SimulateBraking();
            
        }

        public void KeyLeft()
        {
            World.Instance.ControlledCar.KeyLeftPressed = true;
            World.Instance.ControlledCar.MovementTurnLeft();
        }

        public void KeyRight()
        {
            World.Instance.ControlledCar.KeyRightPressed= true;
            World.Instance.ControlledCar.MovementTurnRight();
        }
        public void Space()
        {
            World.Instance.ControlledCar.IsEmergencyBreakOn = true;
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
        public void TransmissionUp()
        {
            
            switch (World.Instance.ControlledCar.CarTransmission)
            {
                case AutomatedCar.Transmissions.P:
                    break;
                case AutomatedCar.Transmissions.R:
                    World.Instance.ControlledCar.TransmissionToP();
                    break;
                case AutomatedCar.Transmissions.N:
                    World.Instance.ControlledCar.TransmissionToR();
                    break;
                case AutomatedCar.Transmissions.D:
                    World.Instance.ControlledCar.TransmissionToN();
                    break;
                default:
                    break;
            }
        }
        public void TransmissionDown()
        {

            switch (World.Instance.ControlledCar.CarTransmission)
            {
                case AutomatedCar.Transmissions.P:
                    World.Instance.ControlledCar.TransmissionToR();
                    break;
                case AutomatedCar.Transmissions.R:
                    World.Instance.ControlledCar.TransmissionToN();
                    break;
                case AutomatedCar.Transmissions.N:
                    World.Instance.ControlledCar.TransmissionToD();
                    break;
                case AutomatedCar.Transmissions.D:
                    break;
                default:
                    break;
            }
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


        public void KeyUpToFalse()
        {
            World.Instance.ControlledCar.KeyUpPressed = false;
        }
        public void KeyDownToFalse()
        {
            World.Instance.ControlledCar.KeyDownPressed = false;
        }
        public void KeyLeftToFalse()
        {
            World.Instance.ControlledCar.KeyLeftPressed = false;
        }
        public void KeyRightToFalse()
        {
            World.Instance.ControlledCar.KeyRightPressed = false;
        }
        public void EmegencBreakToFalse()
        {
            World.Instance.ControlledCar.IsEmergencyBreakOn = false;
        }

    }
}