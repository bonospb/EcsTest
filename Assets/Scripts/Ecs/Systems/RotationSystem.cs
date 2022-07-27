using Fabros.EcsLite.Behaviours;
using Fabros.EcsLite.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Fabros.EcsLite.Ecs.Systems
{
    public class RotationSystem : IEcsRunSystem
    {
        #region Implementation methods
        public void Run(EcsSystems systems)
        {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            var filter = world.Filter<MovementData>().Inc<InputData>().Inc<TransformData>().End();

            var playerDataPool = world.GetPool<MovementData>();
            var inputDataPool = world.GetPool<InputData>();
            var transformDataPool = world.GetPool<TransformData>();

            foreach (var entity in filter)
            {
                ref var playerData = ref playerDataPool.Get(entity);
                ref var inputData = ref inputDataPool.Get(entity);
                ref var transformData = ref transformDataPool.Get(entity);

                var dt = sharedData.TimeService.FixedDeltaTime;
                var speed = playerData.RotationSpeed * dt;

                var targetDir = (inputData.TargetPosition - transformData.Position);
                var forward = (transformData.Rotation * Vector3.forward).normalized;

                float angle = Vector3.SignedAngle(forward, targetDir, Vector3.up);
                if (Mathf.Abs(angle) <= speed)
                    continue;

                transformData.Rotation *= Quaternion.AngleAxis(Mathf.Sign(angle) * speed, Vector3.up);
            }
        }
        #endregion
    }
}
