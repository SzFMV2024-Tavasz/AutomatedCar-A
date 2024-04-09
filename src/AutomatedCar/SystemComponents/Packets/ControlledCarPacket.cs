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

        public double kmhCar
        {
            get => this.kmhcar;
            set => this.RaiseAndSetIfChanged(ref this.kmhcar, value);
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

    }
}
