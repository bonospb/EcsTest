using FreeTeam.Test.Behaviours;
using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class PlayerPointClickInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        #region Constants
        private const string ACTION_BTN_INPUT_NAME = "Action";
        #endregion

        #region Inject
        private readonly EcsFilterInject<Inc<Player, InputData>> filter = default;

        private readonly EcsPoolInject<InputData> inputDataPool = default;
        private readonly EcsPoolInject<TransformData> transformDataPool = default;

        private readonly EcsCustomInject<SceneData> sceneData = default;
        #endregion

        #region Implemetation
        public void Init(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var inputData = ref inputDataPool.Value.Get(entity);

                inputData.TargetPosition = sceneData.Value.PlayerSpawnPointPosition;
            }
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var inputData = ref inputDataPool.Value.Get(entity);
                ref var transformData = ref transformDataPool.Value.Get(entity);

                if (Input.GetButtonUp(ACTION_BTN_INPUT_NAME) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    var playerPlane = new Plane(Vector3.up, transformData.Position);
                    var ray = sceneData.Value.MainCamera.ScreenPointToRay(Input.mousePosition);

                    if (!playerPlane.Raycast(ray, out var hitDistance))
                        continue;

                    inputData.TargetPosition = ray.GetPoint(hitDistance);
                }
            }
        }
        #endregion
    }
}
