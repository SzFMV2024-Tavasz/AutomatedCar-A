namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ControlledCarPacket : ReactiveObject, IReadOnlyControlledCarPacket
    {

        private double kmhcar;
        private double rpmcar;
        private double brakecar;
        private double throttlecar;
        private double steeringWheelRotation;
        private bool emergencybreakonoff;
        private double objectInFrontOfDistance;
        private bool actionRequiredFromDriver;
        public enum Transmissions
        {
            P, // Park
            R, // Reverse
            N, // Neutral
            D,  // Drive
            X //null value, to show nothing if cant transmissionup or down 
        }
        private Transmissions transmission;
        private Transmissions transmissionL;
        private Transmissions transmissionR;

        public Transmissions Transmission
        {
            get => this.transmission;
            set => this.RaiseAndSetIfChanged(ref this.transmission, value);
        }
        public Transmissions TransmissionL
        {
            get => this.transmissionL;
            set => this.RaiseAndSetIfChanged(ref this.transmissionL, value);
        }
        public Transmissions TransmissionR
        {
            get => this.transmissionR;
            set => this.RaiseAndSetIfChanged(ref this.transmissionR, value);
        }
        public double kmhCar
        {
            get => this.kmhcar;
            set => this.RaiseAndSetIfChanged(ref this.kmhcar, value);
        }
        public double SteeringWheelRotation
        {
            get => this.steeringWheelRotation;
            set => this.RaiseAndSetIfChanged(ref this.steeringWheelRotation, value);
        }
        public double ObjectInFrontOfDistance
        {
            get => this.objectInFrontOfDistance;
            set => this.RaiseAndSetIfChanged(ref this.objectInFrontOfDistance, value);
        }
        public bool ActionRequiredFromDriver
        {
            get => this.actionRequiredFromDriver;
            set => this.RaiseAndSetIfChanged(ref this.actionRequiredFromDriver, value);
        }
        public double rpmCar
        {
            get => this.rpmcar;
            set => this.RaiseAndSetIfChanged(ref this.rpmcar, value);
        }
        public double brakeCar
        {
            get => this.brakecar;
            set => this.RaiseAndSetIfChanged(ref this.brakecar, value);
        }
        public double throttleCar
        {
            get => this.throttlecar;
            set => this.RaiseAndSetIfChanged(ref this.throttlecar, value);
        }
        public bool EmergencyBreakOnOff
        {
            get => this.emergencybreakonoff;
            set => this.RaiseAndSetIfChanged(ref this.emergencybreakonoff, value);
        }
    }
}
