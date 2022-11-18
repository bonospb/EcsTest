namespace FreeTeam.Test.Behaviours.Providers
{
    public interface IProvider<T>
    {
        T Value { get; set; }
    }
}
