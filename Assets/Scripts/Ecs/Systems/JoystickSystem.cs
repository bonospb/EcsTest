using Fabros.EcsLite.Behaviours;
using Fabros.EcsLite.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Fabros.EcsLite.Ecs.Systems
{
    public class JoystickSystem : IEcsRunSystem, IEcsInitSystem {
        public void Init(EcsSystems systems)
        {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            var joystickEntity = world.NewEntity();
            var joystickDataPool = world.GetPool<JoystickData>();
            joystickDataPool.Add(joystickEntity);

            ref var joystickData = ref joystickDataPool.Get(joystickEntity);
            var direction = new Vector3(sharedData.SceneData.joystick.Horizontal, 0, sharedData.SceneData.joystick.Vertical);
            joystickData.Direction = direction;
        }

        public void Run (EcsSystems systems) {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            var filter = world.Filter<JoystickData>().Inc<InputData>().End();

            var joystickDataPool = world.GetPool<JoystickData>();
            var inputDataPool = world.GetPool<InputData>();
            
            foreach (var entity in filter)
            {
                ref var joystickData = ref joystickDataPool.Get(entity);
                ref var inputData = ref inputDataPool.Get(entity);
                
                var direction = new Vector3(sharedData.SceneData.joystick.Horizontal, 0, sharedData.SceneData.joystick.Vertical);
                joystickData.Direction = direction;

                if (direction.magnitude > 0)
                {
                    inputData.TargetPosition = Vector3.zero;
                }
            }
        }
    }
}