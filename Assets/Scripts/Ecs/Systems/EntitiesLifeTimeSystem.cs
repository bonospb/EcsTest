using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FreeTeam.Test.Ecs.Systems
{
    public class EntitiesLifeTimeSystem : IEcsRunSystem
    {
        #region Inject
        private readonly EcsFilterInject<Inc<LifeTime>> filter = default;

        private readonly EcsPoolInject<LifeTime> lifeTimePool = default;
        private readonly EcsPoolInject<ProviderReference<float>> providerReferencePool = default;
        private readonly EcsPoolInject<ToDestroy> toDestroyPool = default;

        private readonly EcsCustomInject<ITimeService> timeService = default;
        #endregion

        #region Implementation
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var lifeTime = ref lifeTimePool.Value.Get(entity);
                lifeTime.Value -= timeService.Value.DeltaTime;

                if (providerReferencePool.Value.Has(entity))
                    providerReferencePool.Value.Get(entity).Provider.Value = lifeTime.Value;

                if (lifeTime.Value <= 0)
                    toDestroyPool.Value.Add(entity);
            }
        }
        #endregion
    }
}
