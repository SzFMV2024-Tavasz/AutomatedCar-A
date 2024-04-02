namespace AutomatedCar.Models
{
    using Avalonia.Media;
    using System;
    using System.Runtime.CompilerServices;
    using SystemComponents;

    public class AutomatedCar : Car
    {
        private VirtualFunctionBus virtualFunctionBus;

        public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename)
        {
            this.virtualFunctionBus = new VirtualFunctionBus();
            this.ZIndex = 10;
        }

        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }

        public int Revolution { get; set; }
       
        public double Velocity { get; set; }
        public bool CanGoUp { get; set; } //Check if car can go up or down, or rotate
        public bool CanGoDown { get; set; }
        public bool CanRotate { get; set; }
        public bool KeyUpPressed { get; set; }
        public bool KeyDownPressed { get; set; }
        public bool KeyLeftPressed { get; set; }
        public bool KeyRightPressed { get; set; }


        public PolylineGeometry Geometry { get; set; }

        /// <summary>Starts the automated cor by starting the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Start()
        {
            this.virtualFunctionBus.Start();
        }

        /// <summary>Stops the automated cor by stopping the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Stop()
        {
            this.virtualFunctionBus.Stop();
        }
        /// <summary>
        /// /////////// Custom fuction to move the car
        /// </summary>
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
                World.Instance.ControlledCar.Brake+=20;
            }

            if (World.Instance.ControlledCar.Brake == 0 || World.Instance.ControlledCar.Brake + 5 == 100)
            {
                World.Instance.ControlledCar.Brake+=20;
            }
        }
        public void MovementTurnRight()
        {
            World.Instance.ControlledCar.CanRotate = true;
            int baseValue = (int)World.Instance.ControlledCar.Rotation;
            if (World.Instance.ControlledCar.CanRotate)
            {
                World.Instance.ControlledCar.Rotation += 5;
            }
        }

        public void MovementTurnLeft()
        {
            World.Instance.ControlledCar.CanRotate = true;
            int baseValue = (int)World.Instance.ControlledCar.Rotation;
            if (World.Instance.ControlledCar.CanRotate)
            {
                World.Instance.ControlledCar.Rotation -= 5;
            }
        }
        public void SimulateBraking()
        {
            double brakeIntensity = World.Instance.ControlledCar.Brake;// / 100.0;
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
            //World.Instance.ControlledCar.Throttle -= 10;
        }


        public void MovementForward()
        {
            int baseValue = 25;
            double angleRadians = World.Instance.ControlledCar.Rotation * Math.PI / 180.0;
            double velocity = World.Instance.ControlledCar.Throttle / 100.0;

            int deltaY = (int)(baseValue * velocity * Math.Cos(angleRadians));
            int deltaX = (int)(baseValue * velocity * Math.Sin(angleRadians));
            World.Instance.ControlledCar.X += deltaX;
            World.Instance.ControlledCar.Y -= deltaY;
            World.Instance.ControlledCar.Speed = baseValue * velocity;

            ////old
            //int baseValue = 35;
            //World.Instance.ControlledCar.Velocity = World.Instance.ControlledCar.Throttle / 100.00;
            //double velocity = World.Instance.ControlledCar.Velocity;
            //World.Instance.ControlledCar.Speed = baseValue * velocity;
            //double speed = World.Instance.ControlledCar.Speed;
            //World.Instance.ControlledCar.Y -= (int)speed;
        }
        public void MovementBackward()
        {
            int baseValue = 25;
            double angleRadians = World.Instance.ControlledCar.Rotation * Math.PI / 180.0;
            double velocity = World.Instance.ControlledCar.Throttle / 200.0;

            int deltaY = (int)(baseValue * velocity * Math.Cos(angleRadians));
            int deltaX = (int)(baseValue * velocity * Math.Sin(angleRadians));
            World.Instance.ControlledCar.X -= deltaX;
            World.Instance.ControlledCar.Y += deltaY;
            World.Instance.ControlledCar.Speed = baseValue * velocity;



            //int baseValue = 35;
            //World.Instance.ControlledCar.Velocity = World.Instance.ControlledCar.Brake / 100.00;
            //double velocity = World.Instance.ControlledCar.Velocity;
            //World.Instance.ControlledCar.Speed = baseValue * velocity;
            //double speed = World.Instance.ControlledCar.Speed*0.8; //*0,8 car goes backwards slower
            //World.Instance.ControlledCar.Y += (int)speed;
        }

        public void TransmissionToP()
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
        public void TransmissionToR()
        {
            if (World.Instance.ControlledCar.Velocity == 0)
            {
                World.Instance.ControlledCar.CanGoDown = true;
                World.Instance.ControlledCar.CanGoUp = false;
                World.Instance.ControlledCar.CanRotate = true;
                World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmission.R;
                World.Instance.ControlledCar.CarTransmissionL = AutomatedCar.Transmission.P;
                World.Instance.ControlledCar.CarTransmissionR = AutomatedCar.Transmission.N;
            }
        }
        public void TransmissionToN()
        {
            World.Instance.ControlledCar.CanGoDown = false;
            World.Instance.ControlledCar.CanGoUp = false;
            World.Instance.ControlledCar.CanRotate = true;
            World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmission.N;
            World.Instance.ControlledCar.CarTransmissionL = AutomatedCar.Transmission.R;
            World.Instance.ControlledCar.CarTransmissionR = AutomatedCar.Transmission.D;
        }
        public void TransmissionToD()
        {
            World.Instance.ControlledCar.CanGoDown = false;
            World.Instance.ControlledCar.CanGoUp = true;
            World.Instance.ControlledCar.CanRotate = true;
            World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmission.D;
            World.Instance.ControlledCar.CarTransmissionL = AutomatedCar.Transmission.N;
            World.Instance.ControlledCar.CarTransmissionR = AutomatedCar.Transmission.X;
        }

    }
}