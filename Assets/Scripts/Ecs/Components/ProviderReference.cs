using FreeTeam.Test.Behaviours.Providers;

namespace FreeTeam.Test.Ecs.Components
{
    public struct ProviderReference<T>
    {
        #region Public
        public IProvider<T> Provider;
        #endregion
    }
}
