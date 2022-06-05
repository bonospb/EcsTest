using Fabros.EcsLite.Behaviours;
using Fabros.EcsLite.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Fabros.EcsLite.Ecs.Systems
{
    public class GateOpeningSystem : IEcsRunSystem
    {
        #region Implemetation methods
        public void Run(EcsSystems systems)
        {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            var filter = world.Filter<GateData>().Inc<TransformData>().Inc<GateNeedOpenTag>().End();

            var gateDataPool = world.GetPool<GateData>();
            var transformDataPool = world.GetPool<TransformData>();

            foreach (var entity in filter)
            {
                ref var gateData = ref gateDataPool.Get(entity);
                ref var transformData = ref transformDataPool.Get(entity);

                var dt = sharedData.TimeService.FixedDeltaTime;
                var speed = gateData.OpenSpeed * dt;

                transformData.Position += Vector3.down * speed;
            }
        }
        #endregion
    }
}
