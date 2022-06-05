namespace Fabros.EcsLite.Configurations
{
    public sealed class Configs
    {
        #region Public
        public PlayerConfig PlayerConfig { get; private set; } = new PlayerConfig();
        public GateConfig GateConfig { get; private set; } = new GateConfig();
        #endregion
    }
}
