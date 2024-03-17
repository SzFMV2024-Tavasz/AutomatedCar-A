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
                Accelerate();
                MovementForward();
            }
            if (World.Instance.ControlledCar.CanGoDown)
            {
                World.Instance.ControlledCar.Y += 5;
                //Deccelerte();
                Accelerate();
                movementBackward();
            }
        }

        public void KeyDown()
        {
            Deccelerte();
            SimulateBraking();
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
        public void TransmissionUp()
        {
            
            switch (World.Instance.ControlledCar.CarTransmission)
            {
                case AutomatedCar.Transmission.P:
                    break;
                case AutomatedCar.Transmission.R:
                    TransmissionToP();
                    break;
                case AutomatedCar.Transmission.N:
                    TransmissionToR();
                    break;
                case AutomatedCar.Transmission.D:
                    TransmissionToN();
                    break;
                default:
                    break;
            }
        }
        public void TransmissionDown()
        {
            
            switch (World.Instance.ControlledCar.CarTransmission)
            {
                case AutomatedCar.Transmission.P:
                    TransmissionToR();
                    break;
                case AutomatedCar.Transmission.R:
                    TransmissionToN();
                    break;
                case AutomatedCar.Transmission.N:
                    TransmissionToD();
                    break;
                case AutomatedCar.Transmission.D:
                    break;
                default:
                    break;
            }
        }

         void TransmissionToP()
         {
            if (World.Instance.ControlledCar.Velocity == 0)
            {
                World.Instance.ControlledCar.CanGoUp = false;
                World.Instance.ControlledCar.CanGoDown = false;
                World.Instance.ControlledCar.CanRotate = false;
                World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmission.P;
                World.Instance.ControlledCar.CarTransmissionL = AutomatedCar.Transmission.X;
                World.Instance.ControlledCar.CarTransmissionR = AutomatedCar.Transmission.R;
            }
         }
         void TransmissionToR()
         {
            if (World.Instance.ControlledCar.Velocity==0)
            {
                World.Instance.ControlledCar.CanGoDown = true;
                World.Instance.ControlledCar.CanGoUp = false;
                World.Instance.ControlledCar.CanRotate = true;
                World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmission.R;
                World.Instance.ControlledCar.CarTransmissionL = AutomatedCar.Transmission.P;
                World.Instance.ControlledCar.CarTransmissionR = AutomatedCar.Transmission.N;
            }
         }
         void TransmissionToN()
        {
            World.Instance.ControlledCar.CanGoDown = false;
            World.Instance.ControlledCar.CanGoUp = false;
            World.Instance.ControlledCar.CanRotate = true;
            World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmission.N;
            World.Instance.ControlledCar.CarTransmissionL = AutomatedCar.Transmission.R;
            World.Instance.ControlledCar.CarTransmissionR = AutomatedCar.Transmission.D;
        }
         void TransmissionToD()
        {
            World.Instance.ControlledCar.CanGoDown = false;
            World.Instance.ControlledCar.CanGoUp = true;
            World.Instance.ControlledCar.CanRotate = true;
            World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmission.D;
            World.Instance.ControlledCar.CarTransmissionL = AutomatedCar.Transmission.N;
            World.Instance.ControlledCar.CarTransmissionR = AutomatedCar.Transmission.X;
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

        public void Accelerate()
        {
            // Inceaseing Throttle
            if (World.Instance.ControlledCar.Throttle > 0 && World.Instance.ControlledCar.Throttle < 100)
            {
                World.Instance.ControlledCar.Throttle++;
            }

            if (World.Instance.ControlledCar.Throttle == 0 || World.Instance.ControlledCar.Throttle + 1 == 100)
            {
                World.Instance.ControlledCar.Throttle++;

            }


            // Decreasing Brake
            if (World.Instance.ControlledCar.Brake > 0 && World.Instance.ControlledCar.Brake < 100)
            {
                World.Instance.ControlledCar.Brake--;
            }

            if (World.Instance.ControlledCar.Brake - 1 == 0 || World.Instance.ControlledCar.Brake == 100)
            {
                World.Instance.ControlledCar.Brake--;
            }
        }

        public void Deccelerte()
        {
            // Decreasing Throttle
            if (World.Instance.ControlledCar.Throttle > 0 && World.Instance.ControlledCar.Throttle < 100)
            {
                World.Instance.ControlledCar.Throttle--;
            }

            if (World.Instance.ControlledCar.Throttle - 1 == 0 || World.Instance.ControlledCar.Throttle == 100)
            {
                World.Instance.ControlledCar.Throttle--;
            }

            // Increasing Brake
            if (World.Instance.ControlledCar.Brake > 0 && World.Instance.ControlledCar.Brake < 100)
            {
                World.Instance.ControlledCar.Brake++;
            }

            if (World.Instance.ControlledCar.Brake == 0 || World.Instance.ControlledCar.Brake + 1 == 100)
            {
                World.Instance.ControlledCar.Brake++;
            }
        }
        public void SimulateBraking()
        {
            double brakeIntensity = World.Instance.ControlledCar.Brake / 100.0;
            double velocity = World.Instance.ControlledCar.Velocity;

            if (velocity == 0)
            {
                return;
            }
            double brakingEffect = brakeIntensity * velocity;

            velocity -= brakingEffect;

            if (velocity < 0)
            {
                velocity = 0;
            }
            World.Instance.ControlledCar.Velocity = velocity;
        }


        public void MovementForward()
        {
            int baseValue = 35;
            World.Instance.ControlledCar.Velocity = World.Instance.ControlledCar.Throttle / 100.00;
            double velocity = World.Instance.ControlledCar.Velocity;
            World.Instance.ControlledCar.Speed = baseValue * velocity;
            double speed = World.Instance.ControlledCar.Speed;
            //double velocity = World.Instance.ControlledCar.Throttle / 100.00;
            //int speed = (int)(baseValue * velocity);
            World.Instance.ControlledCar.Y -= (int)speed;
        }

        public void movementBackward()
        {
            int baseValue = 35;
            World.Instance.ControlledCar.Velocity = World.Instance.ControlledCar.Brake / 100.00;
            double velocity = World.Instance.ControlledCar.Velocity;
            World.Instance.ControlledCar.Speed = baseValue * velocity;
            double speed = World.Instance.ControlledCar.Speed*0.8; //*0,8 car goes backwards slower
            World.Instance.ControlledCar.Y += (int)speed;

        }


    }
}