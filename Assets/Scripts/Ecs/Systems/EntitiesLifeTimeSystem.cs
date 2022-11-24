using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FreeTeam.Test.Ecs.Systems
{
    public class EntitiesLifeTimeSystem : IEcsRunSystem
    {
        #region Inject
        private readonly EcsFilterInject<Inc<LifeTime>> filter = default;

        private readonly EcsPoolInject<LifeTime> lifeTimePool = default;
        private readonly EcsPoolInject<ToDestroy> toDestroyPool = default;
        #endregion

        #region Implementation
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var lifeTime = ref lifeTimePool.Value.Get(entity);
                if (lifeTime.Timer > 0)
                    continue;

                toDestroyPool.Value.Add(entity);
            }
        }
        #endregion
    }
}
