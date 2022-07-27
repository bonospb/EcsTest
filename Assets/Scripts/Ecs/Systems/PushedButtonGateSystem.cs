using Fabros.EcsLite.Ecs.Components;
using Leopotam.EcsLite;

namespace Fabros.EcsLite.Ecs.Systems
{
    public class PushedButtonGateSystem : IEcsRunSystem
    {
        #region Implementation methods
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld(); 

            var filter = world.Filter<MovementData>().Inc<TransformData>().End();
            var movementDataPool = world.GetPool<MovementData>();
            var transformDataPool = world.GetPool<TransformData>();

            var buttonFilter = world.Filter<ButtonData>().Inc<TransformData>().End();
            var buttonDataPool = world.GetPool<ButtonData>();

            var gateNeedOpenPool = world.GetPool<GateNeedOpenTag>();

            foreach (var entity in filter)
            {
                ref var playerData = ref movementDataPool.Get(entity);
                ref var playerTransformData = ref transformDataPool.Get(entity);

                foreach (var buttonEntity in buttonFilter)
                {
                    ref var buttonData = ref buttonDataPool.Get(buttonEntity);
                    ref var buttonTransformData = ref transformDataPool.Get(buttonEntity);

                    var distance = (buttonTransformData.Position - playerTransformData.Position).magnitude;
                    foreach (var packedEntity in buttonData.GateEntities)
                    {
                        if (!packedEntity.Unpack(world, out var gateEntity))
                            continue;

                        if (distance <= 1)
                        {
                            if (!gateNeedOpenPool.Has(gateEntity))
                                gateNeedOpenPool.Add(gateEntity);
                        }
                        else
                        {
                            if (gateNeedOpenPool.Has(gateEntity))
                                gateNeedOpenPool.Del(gateEntity);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
