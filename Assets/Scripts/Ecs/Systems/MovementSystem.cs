using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Ecs.Components.Input;
using FreeTeam.Test.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        #region Inject
        private readonly EcsFilterInject<Inc<InputDirection, MovementData, TransformData>> filter = default;

        private readonly EcsPoolInject<InputDirection> inputDirectionPool = default;
        private readonly EcsPoolInject<MovementData> movementDataPool = default;
        private readonly EcsPoolInject<TransformData> transformDataPool = default;
        private readonly EcsPoolInject<IsMoving> isMovingPool = default;

        private readonly EcsCustomInject<ITimeService> timeService = default;
        #endregion

        #region Implemetation
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var inputDirection = ref inputDirectionPool.Value.Get(entity);
                ref var movementData = ref movementDataPool.Value.Get(entity);
                ref var transformData = ref transformDataPool.Value.Get(entity);

                var dt = timeService.Value.DeltaTime;
                var speed = movementData.MoveSpeed * dt;

                var direction = inputDirection.Direction;
                if (direction.magnitude > speed)
                    direction = Vector3.ClampMagnitude(direction, speed);

                if (!Mathf.Approximately(direction.magnitude, 0f))
                    isMovingPool.Value.Add(entity);

                transformData.Position += direction;
            }
        }
        #endregion
    }
}
