namespace AutomatedCar.SystemComponents.Packets
{
    public interface IReadOnlyCollisionPacket
    {
        bool IsCollided { get; }
    }
}