namespace FreeTeam.Test.Configurations
{
    public interface IConfigs
    {
        PlayerConfig PlayerConfig { get; }
        OpponentConfig OpponentConfig { get; }

        FootprintConfig[] FootprintConfigs { get; }
    }
}
