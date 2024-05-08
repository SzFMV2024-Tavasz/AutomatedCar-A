namespace AutomatedCar.Models
{
    using Avalonia.Media;
    using global::AutomatedCar.SystemComponents.Sensors;
    using System;
    using SystemComponents;

    public class AutomatedCar : Car
    {
        private VirtualFunctionBus virtualFunctionBus;
        private Radar radar;
        private Camera camera;
        private Collision collision;

        public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename)
        {
            this.virtualFunctionBus = new VirtualFunctionBus();
            this.ZIndex = 10;
            CarTransmissionL = Transmissions.X;
            CarTransmissionR = Transmissions.R;
            IsEmergencyBreakSafeWorking = true;
            ActionRequiredFromDriver = false;
            if (this is UserControlledCar)
            {
                new ControlledCarSensor(virtualFunctionBus);
                this.camera = new Camera(this.virtualFunctionBus);
                this.collision = new Collision(this.virtualFunctionBus);
                this.radar = new Radar(this.virtualFunctionBus);
            }
            
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
        public double Rpm { get; set; }
        public int Gear{ get; set; }
        public const int eBrakeMax = 75;
        public bool CanGoUp { get; set; } //Check if car can go up or down, or rotate
        public bool CanGoDown { get; set; }
        public bool CanRotate { get; set; }
        public bool KeyUpPressed { get; set; }
        public bool KeyDownPressed { get; set; }
        public bool KeyLeftPressed { get; set; }
        public bool KeyRightPressed { get; set; }
        public bool IsEmergencyBreakOn { get; set; }
        public double ObjectInFrontOfDistance { get;set; }
        public bool ActionRequiredFromDriver { get; set; }
        public bool IsEmergencyBreakSafeWorking { get; set; }
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
        public void Accelerate()
        {

            if (World.Instance.ControlledCar.Rpm > 2500 && World.Instance.ControlledCar.Rpm < 2600)
            {
                if (World.Instance.ControlledCar.Gear < 4)
                {
                    World.Instance.ControlledCar.Gear++;
                    World.Instance.ControlledCar.Rpm = 1800;
                }
            }
            if (World.Instance.ControlledCar.Rpm < 1500 && World.Instance.ControlledCar.Rpm > 1400)
            {
                World.Instance.ControlledCar.Gear--;
            }

            // Inceaseing Throttle
            if (World.Instance.ControlledCar.Throttle > 0 && World.Instance.ControlledCar.Throttle < 100)
            {
                World.Instance.ControlledCar.Throttle++;
                if (World.Instance.ControlledCar.Rpm < 3700)
                {
                    World.Instance.ControlledCar.Rpm += Throttle * 2;
                }
            }

            if (World.Instance.ControlledCar.Throttle == 0 || World.Instance.ControlledCar.Throttle + 1 == 100)
            {
                World.Instance.ControlledCar.Throttle++;
                if (World.Instance.ControlledCar.Rpm < 3700)
                {
                    World.Instance.ControlledCar.Rpm += Throttle * 3;
                }
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
                if(World.Instance.ControlledCar.Throttle < 0)
                {
                    World.Instance.ControlledCar.Throttle = 0;
                }
            }


            if (World.Instance.ControlledCar.Rpm > 0)
            {
                World.Instance.ControlledCar.Rpm -= World.Instance.ControlledCar.Throttle * 3;
                if (World.Instance.ControlledCar.Rpm < 0)
                {
                    World.Instance.ControlledCar.Rpm = 0;
                }
            }

            if (World.Instance.ControlledCar.Throttle - 1 == 0 || World.Instance.ControlledCar.Throttle == 100)
            {
                World.Instance.ControlledCar.Throttle--;
            }

            // Increasing Brake
            if (World.Instance.ControlledCar.Brake > 0 && World.Instance.ControlledCar.Brake < 100)
            {
                World.Instance.ControlledCar.Brake+=5;
                World.Instance.ControlledCar.Brake += 10;
            }

            if (World.Instance.ControlledCar.Brake == 0 || World.Instance.ControlledCar.Brake + 5 == 100)
            {
                World.Instance.ControlledCar.Brake+=5;
                World.Instance.ControlledCar.Brake += 10;
            }
        }
        public void MovementTurnRight()
        {
            if (World.Instance.ControlledCar.Velocity > 0)
            {
                World.Instance.ControlledCar.CanRotate = true;
                double baseValue = 0.5;
                World.Instance.ControlledCar.Rotation += baseValue * SteeringWheelRotation;
                if (World.Instance.ControlledCar.SteeringWheelRotation < 60)
                {
                    World.Instance.ControlledCar.SteeringWheelRotation++;
                }
            }
        }
        public void MovementTurnLeft()
        {
            if (World.Instance.ControlledCar.Velocity > 0)
            {
                World.Instance.ControlledCar.CanRotate = true;
                double baseValue = 0.5;
                World.Instance.ControlledCar.Rotation += baseValue * SteeringWheelRotation;
                if (World.Instance.ControlledCar.SteeringWheelRotation > -60)
                {
                    World.Instance.ControlledCar.SteeringWheelRotation--;
                }
            }
        }
        
        public void SimulateBraking()
        {
            double brakeIntensity = World.Instance.ControlledCar.Brake;
            double velocity = World.Instance.ControlledCar.Speed;

            if (velocity == 0)
            {
                return;
            }

            velocity *= 1 - (brakeIntensity / 100);

            if (velocity < 0)
            {
                velocity = 0;
            }
            World.Instance.ControlledCar.Speed = velocity;
            World.Instance.ControlledCar.Velocity = velocity;
        }

        public void SetSensors()
        {
            this.camera.RelativeLocation = new Avalonia.Point(this.Geometry.Bounds.Center.X, this.Geometry.Bounds.Center.Y / 2);
            this.radar.RelativeLocation = new Avalonia.Point(this.Geometry.Bounds.Center.X, this.Geometry.Bounds.Center.Y / 2);
            this.collision.RelativeLocation = new Avalonia.Point(this.Geometry.Bounds.Center.X, this.Geometry.Bounds.Center.Y / 2);
        }

        public void MovementForward()
        {
            
            int pixelsPerKm = 50 * 1000;

            double angleRadians = World.Instance.ControlledCar.Rotation * Math.PI / 180.0;
            double velocity;

            if (KeyDownPressed)
            {
                velocity = World.Instance.ControlledCar.Speed/pixelsPerKm;
                KeyDownPressed = false;
            }
            else
            {
                velocity = World.Instance.ControlledCar.Throttle / 100.0;
            }
            double speedMeterPerSecond = velocity * pixelsPerKm / 3600.0; 

            double speedKmPerHour = velocity * 3600.0; 

            double deltaY = (speedMeterPerSecond * velocity * Math.Cos(angleRadians));
            double deltaX = (speedMeterPerSecond * velocity * Math.Sin(angleRadians));
            World.Instance.ControlledCar.X += deltaX;
            World.Instance.ControlledCar.Y -= deltaY;
            if (speedKmPerHour < 0)
            {
                World.Instance.ControlledCar.Speed = 0;
                World.Instance.ControlledCar.Velocity = 0;
            }
            else
            {
                World.Instance.ControlledCar.Speed = speedKmPerHour;
                World.Instance.ControlledCar.Velocity = speedKmPerHour / 36;
                if (World.Instance.ControlledCar.Velocity > 60)
                {
                    IsEmergencyBreakSafeWorking = false;
                }
                else
                {
                    IsEmergencyBreakSafeWorking = true;
                }
            }
            
        }
        public void MovementBackward()
        {
            int pixelsPerKm = 50 * 1000;

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
            double speedMeterPerSecond = velocity * pixelsPerKm / 3600.0;

            double speedKmPerHour = velocity * 3600.0;

            int deltaY = (int)(speedMeterPerSecond * velocity * Math.Cos(angleRadians));
            int deltaX = (int)(speedMeterPerSecond * velocity * Math.Sin(angleRadians));
            World.Instance.ControlledCar.X -= deltaX;
            World.Instance.ControlledCar.Y += deltaY;
            if (speedKmPerHour < 0)
            {
                World.Instance.ControlledCar.Speed = 0;
                World.Instance.ControlledCar.Velocity = 0;
            }
            else
            {
                World.Instance.ControlledCar.Speed = speedKmPerHour;
                World.Instance.ControlledCar.Velocity = speedKmPerHour / 36;
            }
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
        public WorldObject DetectObjInFrontOfTheCar()
        {
            //check if object is in front of the car
            //var closestObjectToCar = this.virtualFunctionBus.RadarPacket.ClosestObject;
            //WorldObject obj=closestObjectToCar.GetRelevantObject();

            var relevantObjectToCar = this.virtualFunctionBus.RadarPacket.RelevantObjects;
            //WorldObject obj = relevantObjectToCar.;
            foreach (var item in relevantObjectToCar)
            {
                WorldObject obj = item.GetRelevantObject();

                if (obj != null && obj.Collideable)
                {
                    //ControlledCar position
                    double x1 = this.X;
                    double y1 = this.Y;

                    //Relevant object position
                    double x2 = obj.X;
                    double y2 = obj.Y;

                    // Ellenõrizzük, hogy a (x2, y2) pont rajta van-e a vektoron
                    double angle = (90 + World.Instance.ControlledCar.Rotation) * Math.PI / 180.0;

                    for (int i = -90; i < 90; i++)
                    {
                        if (IsPointOnVector(x1, y1, angle, x2 + i, y2))
                        {
                            //objektum az autó elõtt van
                            return obj;
                        }
                    }
                    for (int j = -90; j < 90; j++)
                    {
                        if (IsPointOnVector(x1, y1, angle, x2, y2 + j))
                        {
                            //objektum az autó elõtt van
                            return obj;
                        }
                    }
                }
            }
            return null;
        }

        public bool IsPointOnVector(double x1, double y1, double angle, double x2, double y2)
        {
            // A vektor irányvektorának kiszámítása
            double directionX = Math.Cos(angle);
            double directionY = Math.Sin(angle);

            // A pont és az eredeti pont közötti vektor kiszámítása
            double vectorX = x2 - x1;
            double vectorY = y2 - y1;

            // Skaláris szorzat kiszámítása
            double dotProduct = vectorX * directionX + vectorY * directionY;

            // Ha a skaláris szorzat 0, akkor a pontok merõlegesek, tehát nem esnek egy vonalra
            if (dotProduct == 0)
                return false;

            // Az irányvektor és a pont közötti szög kiszámítása
            double vectorMagnitude = Math.Sqrt(vectorX * vectorX + vectorY * vectorY);
            double cosTheta = dotProduct / (vectorMagnitude * Math.Sqrt(directionX * directionX + directionY * directionY));

            // Ha a cosTheta közel 1 vagy -1, akkor a pont rajta van a vektoron
            return Math.Abs(cosTheta - 1) < 0.000001 || Math.Abs(cosTheta + 1) < 0.000001;
        }

        public bool CheckSafeDistance(double dist)
        {
            double ratio = dist / Velocity;
            if (ratio<7)
            {
                ActionRequiredFromDriver = true;
                if (ratio<4)
                {
                    IsEmergencyBreakOn = true;
                }
                return false;
            }
            else
            {
                ActionRequiredFromDriver = false;
                return true;
            }
        }
        public void EmergencyBreak()
        {
            if (World.Instance.ControlledCar.Throttle>1) //World.Instance.ControlledCar.Velocity > 1&&
            {
                if (Velocity <= eBrakeMax)
                {
                    Brake = Velocity + 5;
                }
                else
                {
                    Brake = eBrakeMax;
                }
                if (Throttle-50 > 0)
                {
                    Throttle -= 50;
                }
                
                
               

            }
            World.Instance.ControlledCar.IsEmergencyBreakOn = false;
            //World.Instance.ControlledCar.Throttle = 0;
            //World.Instance.ControlledCar.Velocity = 0;
        }


    }
}