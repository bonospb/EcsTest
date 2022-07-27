namespace FreeTeam.Test.Configurations
{
    public sealed class Configs
    {
        #region Public
        public PlayerConfig PlayerConfig { get; private set; } = new PlayerConfig();
        public OpponentConfig OpponentConfig { get; private set; } = new OpponentConfig();

        public GateConfig GateConfig { get; private set; } = new GateConfig();
        #endregion
    }
}
