namespace FreeTeam.Test.Common
{
    internal interface ITimeService
    {
        float DeltaTime { get; }
        float FixedDeltaTime { get; }
    }
}
