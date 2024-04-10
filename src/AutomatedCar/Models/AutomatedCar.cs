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
            CarTransmissionL = Transmissions.X;
            CarTransmissionR = Transmissions.R;
            new ControlledCarSensor(virtualFunctionBus);
        }

        
        public enum Transmissions
        {
            P, // Park
            R, // Reverse
            N, // Neutral
            D,  // Drive
            X //null value, to show nothing if cant transmissionup or down 
        }
        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }



        public int Revolution { get; set; }
       
        public double Velocity { get; set; }
        public double Throttle { get; set; }
        public double Brake { get; set; }
        public double SteeringWheelRotation { get; set; }
        
        public bool CanGoUp { get; set; } //Check if car can go up or down, or rotate
        public bool CanGoDown { get; set; }
        public bool CanRotate { get; set; }
        public bool KeyUpPressed { get; set; }
        public bool KeyDownPressed { get; set; }
        public bool KeyLeftPressed { get; set; }
        public bool KeyRightPressed { get; set; }
        public bool IsEmergencyBreakOn { get; set; }

        public Transmissions CarTransmission { get; set; }
        public Transmissions CarTransmissionL { get; set; }
        public Transmissions CarTransmissionR { get; set; }

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
                World.Instance.ControlledCar.Brake+=10;
            }

            if (World.Instance.ControlledCar.Brake == 0 || World.Instance.ControlledCar.Brake + 10 == 100)
            {
                World.Instance.ControlledCar.Brake+=10;
            }
        }

        public void MovementTurnRight()
{
    if (World.Instance.ControlledCar.Velocity > 0)
    {
        World.Instance.ControlledCar.CanRotate = true;
        World.Instance.ControlledCar.Rotation += SteeringWheelRotation / 5;
        if (World.Instance.ControlledCar.SteeringWheelRotation < 65)
        {
            World.Instance.ControlledCar.SteeringWheelRotation += 5;
        }
    }
    
}

        public void MovementTurnLeft()
{
    if (World.Instance.ControlledCar.Velocity > 0)
    {
        World.Instance.ControlledCar.CanRotate = true;
        World.Instance.ControlledCar.Rotation += SteeringWheelRotation / 5;
        if (World.Instance.ControlledCar.SteeringWheelRotation > -65)
        {
            World.Instance.ControlledCar.SteeringWheelRotation -= 5;
        }
    }
}

        public void SimulateBraking()
        {
            double brakeIntensity = World.Instance.ControlledCar.Brake;// / 100.0;
            double velocity = World.Instance.ControlledCar.Speed;

            if (velocity == 0)
            {
                return;
            }

            velocity *=1-(brakeIntensity/100);

            if (velocity < 0)
            {
                velocity = 0;
            }
            World.Instance.ControlledCar.Speed = velocity;
        }

        public void MovementForward()
        {
            int pixelsPerKm = 50 * 1000; // 1 km = 50 * 1000 pixel

            double angleRadians = World.Instance.ControlledCar.Rotation * Math.PI / 180.0;
            double velocity;

            if (KeyDownPressed)
            {
                velocity = World.Instance.ControlledCar.Speed / pixelsPerKm;
                KeyDownPressed = false;
            }
            else
            {
                velocity = World.Instance.ControlledCar.Throttle / 100.0;
            }

            // Sz�m�tsuk ki a sebess�get m�ter/m�sodpercben
            double speedMeterPerSecond = velocity * pixelsPerKm / 3600.0; // A sebess�get m�ter/m�sodpercbe konvert�ljuk

            // �tv�lt�s km/h-ra
            double speedKmPerHour = velocity * 3600.0; // m/s-b�l km/h-ba konvert�lunk

            int deltaY = (int)(speedMeterPerSecond * velocity * Math.Cos(angleRadians));
            int deltaX = (int)(speedMeterPerSecond * velocity * Math.Sin(angleRadians));
            World.Instance.ControlledCar.X += deltaX;
            World.Instance.ControlledCar.Y -= deltaY;
            World.Instance.ControlledCar.Speed = speedKmPerHour; // Az objektum sebess�ge km/h-ban
            World.Instance.ControlledCar.Velocity = speedKmPerHour/36; // Az objektum gyorsul�sa km/h-ban
        }
        //public void MovementForward()
        //{
        //    int baseValue = 25;
        //    double angleRadians = World.Instance.ControlledCar.Rotation * Math.PI / 180.0;
        //    double velocity;
        //    if (KeyDownPressed)
        //    { 
        //         velocity=World.Instance.ControlledCar.Speed/100;
        //        KeyDownPressed = false;
        //    }
        //    else
        //    {
        //        velocity = World.Instance.ControlledCar.Throttle / 100.0;
        //    }
        //    int deltaY = (int)(baseValue * velocity * Math.Cos(angleRadians));
        //    int deltaX = (int)(baseValue * velocity * Math.Sin(angleRadians));
        //    World.Instance.ControlledCar.X += deltaX;
        //    World.Instance.ControlledCar.Y -= deltaY;
        //    World.Instance.ControlledCar.Speed = baseValue * velocity;
        //}
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
        }

        public void TransmissionToP()
        {
            if (World.Instance.ControlledCar.Speed == 0)
            {
                World.Instance.ControlledCar.CanGoUp = false;
                World.Instance.ControlledCar.CanGoDown = false;
                World.Instance.ControlledCar.CanRotate = false;
                World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmissions.P;
                World.Instance.ControlledCar.CarTransmissionL = AutomatedCar.Transmissions.X;
                World.Instance.ControlledCar.CarTransmissionR = AutomatedCar.Transmissions.R;
            }
        }
        public void TransmissionToR()
        {
            if (World.Instance.ControlledCar.Speed == 0)
            {
                World.Instance.ControlledCar.CanGoDown = true;
                World.Instance.ControlledCar.CanGoUp = false;
                World.Instance.ControlledCar.CanRotate = true;
                World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmissions.R;
                World.Instance.ControlledCar.CarTransmissionL = AutomatedCar.Transmissions.P;
                World.Instance.ControlledCar.CarTransmissionR = AutomatedCar.Transmissions.N;
            }
        }
        public void TransmissionToN()
        {
            World.Instance.ControlledCar.CanGoDown = false;
            World.Instance.ControlledCar.CanGoUp = false;
            World.Instance.ControlledCar.CanRotate = true;
            World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmissions.N;
            World.Instance.ControlledCar.CarTransmissionL = AutomatedCar.Transmissions.R;
            World.Instance.ControlledCar.CarTransmissionR = AutomatedCar.Transmissions.D;
        }
        public void TransmissionToD()
        {
            World.Instance.ControlledCar.CanGoDown = false;
            World.Instance.ControlledCar.CanGoUp = true;
            World.Instance.ControlledCar.CanRotate = true;
            World.Instance.ControlledCar.CarTransmission = AutomatedCar.Transmissions.D;
            World.Instance.ControlledCar.CarTransmissionL = AutomatedCar.Transmissions.N;
            World.Instance.ControlledCar.CarTransmissionR = AutomatedCar.Transmissions.X;
        }

    }
}