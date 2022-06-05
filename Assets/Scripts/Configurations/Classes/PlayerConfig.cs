namespace Fabros.EcsLite.Configurations
{
    public sealed class PlayerConfig
    {
        #region Public
        public float PlayerMoveSpeed { get; private set; } = 5;
        public float PlayerRotationSpeed { get; private set; } = 270;
        #endregion
    }
}
