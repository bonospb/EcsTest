using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FreeTeam.Test.Ecs.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        #region Inject
        private readonly EcsFilterInject<Inc<MovementData, InputData, TransformData>> filter = default;

        private readonly EcsPoolInject<MovementData> movementDataPool = default;
        private readonly EcsPoolInject<InputData> inputDataPool = default;
        private readonly EcsPoolInject<TransformData> transformDataPool = default;

        private readonly EcsCustomInject<ITimeService> timeService = default;
        #endregion

        #region Implemetation
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var movementData = ref movementDataPool.Value.Get(entity);
                ref var inputData = ref inputDataPool.Value.Get(entity);
                ref var transformData = ref transformDataPool.Value.Get(entity);

                var dt = timeService.Value.FixedDeltaTime;
                var speed = movementData.MoveSpeed * dt;

                var direction = (inputData.TargetPosition - transformData.Position);
                if (direction.magnitude <= speed)
                    continue;

                transformData.Position += direction.normalized * speed;
            }
        }
        #endregion
    }
}
