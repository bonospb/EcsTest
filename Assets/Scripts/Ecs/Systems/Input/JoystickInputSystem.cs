using FreeTeam.Test.Behaviours;
using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Ecs.Components.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class JoystickInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        #region Inject
        private readonly EcsFilterInject<Inc<Player, TransformData>> filter = default;

        private readonly EcsPoolInject<TransformData> transformDataPool = default;
        private readonly EcsPoolInject<InputTargetPoint> inputTargetPointPool = default;

        private readonly EcsCustomInject<SceneContext> sceneContex = default;
        #endregion

        #region Private
        private Camera camera = null;
        #endregion

        #region Implementation
        public void Init(IEcsSystems systems)
        {
            camera = Camera.main;
        }

        public void Run(IEcsSystems systems)
        {
            var joystick = sceneContex.Value.HUD.Joystick;
            var joystickDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

            if (Mathf.Approximately(joystickDirection.magnitude, 0f))
                return;

            foreach (var entity in filter.Value)
            {
                var position = transformDataPool.Value.Get(entity).Position;
                var direction = Quaternion.AngleAxis(camera.transform.eulerAngles.y, Vector3.up) * joystickDirection;

                inputTargetPointPool.Value.Replace(entity).TargetPoint = position + direction;
            }
        }
        #endregion
    }
}
