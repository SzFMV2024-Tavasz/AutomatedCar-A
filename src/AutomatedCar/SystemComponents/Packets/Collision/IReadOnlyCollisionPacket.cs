namespace AutomatedCar.SystemComponents.Packets.Collision
{
    public interface IReadOnlyCollisionPacket
    {
        bool IsCollided { get; }
    }
}