namespace FreeTeam.Test.Services
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        float FixedDeltaTime { get; }
    }
}
