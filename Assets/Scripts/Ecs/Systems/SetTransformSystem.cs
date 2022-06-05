using Fabros.EcsLite.Ecs.Components;
using Leopotam.EcsLite;

namespace Fabros.EcsLite.Ecs.Systems
{
    public class SetTransformSystem : IEcsRunSystem
    {
        #region Implementation methods
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            var filter = world.Filter<PlayerData>().Inc<InputData>().Inc<TransformData>().End();

            var transformDataPool = world.GetPool<TransformData>();
            var transformReferencePool = world.GetPool<TransformReference>();

            foreach (var entity in filter)
            {
                ref var transformData = ref transformDataPool.Get(entity);
                ref var transformReference = ref transformReferencePool.Get(entity);

                transformReference.Transform.SetPositionAndRotation(transformData.Position, transformData.Rotation);
            }
        }
        #endregion
    }
}
