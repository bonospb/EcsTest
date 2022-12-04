using FreeTeam.Test.Behaviours;
using FreeTeam.Test.Common;
using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Ecs.Components.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class KeyboardInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        #region Inject
        private readonly EcsFilterInject<Inc<Player, TransformData>> filter = default;

        private readonly EcsPoolInject<TransformData> transformDataPool = default;
        private readonly EcsPoolInject<InputTargetPoint> inputTargetPointPool = default;
        #endregion

        #region Private
        private Camera camera = null;
        #endregion

        #region Implementation
        public void Init(IEcsSystems systems) =>
            camera = Camera.main;

        public void Run(IEcsSystems systems)
        {
            var horizontal = Input.GetAxis(InputConstants.HORIZONTAL_AXIS_NAME);
            var vertical = Input.GetAxis(InputConstants.VERTICAL_AXIS_NAME);

            var joystickDirection = new Vector3(horizontal, 0, vertical);

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
