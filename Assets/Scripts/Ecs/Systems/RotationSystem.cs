using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Ecs.Components.Input;
using FreeTeam.Test.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class RotationSystem : IEcsRunSystem
    {
        #region Inject
        private readonly EcsFilterInject<Inc<MovementData, InputDirection, TransformData>> filter = default;

        private readonly EcsPoolInject<MovementData> movementDataPool = default;
        private readonly EcsPoolInject<InputDirection> inputDirectionPool = default;
        private readonly EcsPoolInject<TransformData> transformDataPool = default;

        private readonly EcsCustomInject<ITimeService> timeService = default;
        #endregion

        #region Implementation
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var playerData = ref movementDataPool.Value.Get(entity);
                ref var inputDirection = ref inputDirectionPool.Value.Get(entity);
                ref var transformData = ref transformDataPool.Value.Get(entity);

                var dt = timeService.Value.DeltaTime;

                var targetDir = inputDirection.Direction.normalized;
                var angle = Vector3.SignedAngle(transformData.Direction, targetDir, Vector3.up);

                var rotationSpeed = Mathf.Min(playerData.RotationSpeed * dt, Mathf.Abs(angle));
                var direction = (Quaternion.AngleAxis(Mathf.Sign(angle) * rotationSpeed, Vector3.up) * transformData.Direction);

                transformData.Direction = direction;
            }
        }
        #endregion
    }
}
