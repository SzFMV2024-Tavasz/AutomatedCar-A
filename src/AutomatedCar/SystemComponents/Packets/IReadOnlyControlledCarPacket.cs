namespace AutomatedCar.SystemComponents.Packets
{
    using System.Dynamic;

    public interface IReadOnlyControlledCarPacket
    {
        double kmhCar { get; }
        double rpmCar { get; }
        double brakeCar{ get; }
        double throttleCar { get; }
        double SteeringWheelRotation { get; }
        Packets.ControlledCarPacket.Transmissions Transmission { get; }
        Packets.ControlledCarPacket.Transmissions TransmissionL { get; }
    Packets.ControlledCarPacket.Transmissions TransmissionR { get; }
    }
}