using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FreeTeam.Test.Ecs.Systems
{
    public class SetProgressSystem : IEcsRunSystem
    {
        #region Inject
        private readonly EcsFilterInject<Inc<ProgressData, ProviderReference<float>>> filter = default;

        private readonly EcsPoolInject<ProgressData> progressDataPool = default;
        private readonly EcsPoolInject<ProviderReference<float>> providerReferencePool = default;
        #endregion

        #region Implementation
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var progressData = ref progressDataPool.Value.Get(entity);
                ref var progressReference = ref providerReferencePool.Value.Get(entity);

                progressReference.Provider.Value = progressData.Progress;
            }
        }
        #endregion
    }
}
