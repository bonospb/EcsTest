using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;

namespace FreeTeam.Test.Ecs.Systems
{
    public class SetTransformSystem : IEcsRunSystem
    {
        #region Implementation methods
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            var filter = world
                .Filter<TransformData>()
                .Inc<TransformReference>()
                .End();

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
