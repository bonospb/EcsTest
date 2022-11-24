using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FreeTeam.Test.Ecs.Systems
{
    public class DestroyEntitiesSystem : IEcsRunSystem
    {
        #region Inject
        private readonly EcsWorldInject world;

        private readonly EcsFilterInject<Inc<ToDestroy>> filter = default;
        #endregion

        #region Implementation
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
                world.Value.DelEntity(entity);
        }
        #endregion
    }
}
