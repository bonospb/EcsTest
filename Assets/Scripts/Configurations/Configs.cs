namespace Fabros.EcsLite.Configurations
{
    public class Configs
    {
        #region Public
        public PlayerConfig PlayerConfig { get; private set; } = new PlayerConfig();
        public GateConfig GateConfig { get; private set; } = new GateConfig();
        #endregion
    }
}
