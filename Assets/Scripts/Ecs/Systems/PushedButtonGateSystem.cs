using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FreeTeam.Test.Ecs.Systems
{
    public class PushedButtonGateSystem : IEcsRunSystem
    {
        #region Inject
        private readonly EcsFilterInject<Inc<MovementData, TransformData>> unitFilter = default;
        private readonly EcsFilterInject<Inc<ButtonData, TransformData>, Exc<IsButtonPushed>> buttonFilter = default;

        private readonly EcsPoolInject<MovementData> movementDataPool = default;
        private readonly EcsPoolInject<TransformData> transformDataPool = default;
        private readonly EcsPoolInject<ButtonData> buttonDataPool = default;
        private readonly EcsPoolInject<IsButtonPushed> isButtonPushedPool = default;
        #endregion

        #region Implementation
        public void Run(IEcsSystems systems)
        {
            foreach (var unityEntity in unitFilter.Value)
            {
                ref var movementData = ref movementDataPool.Value.Get(unityEntity);
                ref var unitTransformData = ref transformDataPool.Value.Get(unityEntity);

                foreach (var buttonEntity in buttonFilter.Value)
                {
                    ref var buttonData = ref buttonDataPool.Value.Get(buttonEntity);
                    ref var buttonTransformData = ref transformDataPool.Value.Get(buttonEntity);

                    var distance = (buttonTransformData.Position - unitTransformData.Position).magnitude;

                    var isButtonPushed = distance < 1f;
                    if (isButtonPushed && !isButtonPushedPool.Value.Has(buttonEntity))
                        isButtonPushedPool.Value.Add(buttonEntity);
                }
            }
        }
        #endregion
    }
}
