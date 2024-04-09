namespace AutomatedCar.SystemComponents.Packets
{
    using System.Dynamic;

    public interface IReadOnlyControlledCarPacket
    {
        double kmhCar { get; }
        double rpmCar { get; }
        double brakeCar{ get; }
        double throttleCar { get; }
    }
}