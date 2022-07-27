using Fabros.EcsLite.Behaviours;
using Fabros.EcsLite.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Fabros.EcsLite.Ecs.Systems
{
    public class PlayerInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        #region Implemetation methods
        public void Init(EcsSystems systems)
        {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            var filter = world.Filter<Player>().Inc<InputData>().End();

            var inputDataPool = world.GetPool<InputData>();

            foreach (var entity in filter)
            {
                ref var inputData = ref inputDataPool.Get(entity);

                inputData.TargetPosition = sharedData.SceneData.PlayerSpawnPointPosition;
            }
        }

        public void Run(EcsSystems systems)
        {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            var filter = world
                .Filter<Player>()
                .Inc<InputData>()
                .Inc<TransformData>()
                .End();

            var inputDataPool = world.GetPool<InputData>();
            var transformDataPool = world.GetPool<TransformData>();

            foreach (var entity in filter)
            {
                ref var inputData = ref inputDataPool.Get(entity);
                ref var transformData = ref transformDataPool.Get(entity);

                if (Input.GetButtonUp("Action") && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    Plane playerPlane = new Plane(Vector3.up, transformData.Position);
                    Ray ray = sharedData.SceneData.MainCamera.ScreenPointToRay(Input.mousePosition);

                    if (!playerPlane.Raycast(ray, out var hitDistance))
                        continue;

                    inputData.TargetPosition = ray.GetPoint(hitDistance);
                }
            }
        }
        #endregion
    }
}
