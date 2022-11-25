namespace FreeTeam.Test.Configurations
{
    public interface IConfigs
    {
        PlayerConfig PlayerConfig { get; }
        OpponentConfig OpponentConfig { get; }

        GateConfig GateConfig { get; }

        FootprintConfig[] FootprintConfigs { get; }
    }
}
