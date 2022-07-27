using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;

namespace FreeTeam.Test.Ecs.Systems
{
    public class PushedButtonGateSystem : IEcsRunSystem
    {
        #region Implementation methods
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld(); 

            var unitfilter = world.Filter<MovementData>().Inc<TransformData>().End();
            var buttonFilter = world.Filter<ButtonData>().Inc<TransformData>().Exc<IsButtonPushed>().End();

            var movementDataPool = world.GetPool<MovementData>();
            var transformDataPool = world.GetPool<TransformData>();
            var buttonDataPool = world.GetPool<ButtonData>();

            var isButtonPushedPool = world.GetPool<IsButtonPushed>();

            foreach (var unityEntity in unitfilter)
            {
                ref var movementData = ref movementDataPool.Get(unityEntity);
                ref var unitTransformData = ref transformDataPool.Get(unityEntity);

                foreach (var buttonEntity in buttonFilter)
                {
                    ref var buttonData = ref buttonDataPool.Get(buttonEntity);
                    ref var buttonTransformData = ref transformDataPool.Get(buttonEntity);

                    var distance = (buttonTransformData.Position - unitTransformData.Position).magnitude;

                    var isButtonPushed = distance < 1f;
                    if (isButtonPushed && !isButtonPushedPool.Has(buttonEntity))
                        isButtonPushedPool.Add(buttonEntity);
                }
            }
        }
        #endregion
    }
}
