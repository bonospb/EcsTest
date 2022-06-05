using Fabros.EcsLite.Behaviours;
using Fabros.EcsLite.Ecs.Components;
using Leopotam.EcsLite;

namespace Fabros.EcsLite.Ecs.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        #region Implemetation methods
        public void Run(EcsSystems systems)
        {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            var filter = world.Filter<PlayerData>().Inc<InputData>().Inc<TransformData>().End();

            var playerDataPool = world.GetPool<PlayerData>();
            var inputDataPool = world.GetPool<InputData>();
            var transformDataPool = world.GetPool<TransformData>();

            foreach (var entity in filter)
            {
                ref var playerData = ref playerDataPool.Get(entity);
                ref var inputData = ref inputDataPool.Get(entity);
                ref var transformData = ref transformDataPool.Get(entity);

                var dt = sharedData.TimeService.FixedDeltaTime;
                var speed = playerData.MoveSpeed * dt;

                var direction = (inputData.TargetPosition - transformData.Position);
                if (direction.magnitude <= speed)
                    continue;

                transformData.Position += direction.normalized * speed;
            }
        }
        #endregion
    }
}
