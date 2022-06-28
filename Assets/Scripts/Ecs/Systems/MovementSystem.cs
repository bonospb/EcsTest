using Fabros.EcsLite.Behaviours;
using Fabros.EcsLite.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Fabros.EcsLite.Ecs.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        #region Implemetation methods
        public void Run(EcsSystems systems)
        {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            var filter = world.Filter<PlayerData>().Inc<InputData>().Inc<TransformData>().Inc<JoystickData>().End();

            var playerDataPool = world.GetPool<PlayerData>();
            var inputDataPool = world.GetPool<InputData>();
            var transformDataPool = world.GetPool<TransformData>();

            var joystickDataPool = world.GetPool<JoystickData>();

            foreach (var entity in filter)
            {
                ref var playerData = ref playerDataPool.Get(entity);
                ref var inputData = ref inputDataPool.Get(entity);
                ref var transformData = ref transformDataPool.Get(entity);
                ref var joystickData = ref joystickDataPool.Get(entity);

                var dt = sharedData.TimeService.FixedDeltaTime;
                var speed = playerData.MoveSpeed * dt;

                Vector3 direction = Vector3.zero;
                
                if (joystickData.Direction.magnitude > 0)
                {
                    direction = joystickData.Direction;
                    Debug.LogFormat("Direction from joystick: {0} {1}", direction.x, direction.z);
                }
                else if (inputData.TargetPosition != Vector3.zero)
                {
                    direction = (inputData.TargetPosition - transformData.Position);
                    Debug.LogFormat("Direction from mouse: {0} {1}", direction.x, direction.z);
                }
                if (direction.magnitude <= speed)
                    continue;

                transformData.Position += direction.normalized * speed;
            }
        }
        #endregion
    }
}
