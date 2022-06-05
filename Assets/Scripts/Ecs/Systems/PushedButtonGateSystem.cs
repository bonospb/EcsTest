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

            var playerFilter = world.Filter<PlayerData>().Inc<TransformData>().End();
            var playerDataPool = world.GetPool<PlayerData>();
            var playerTransformDataPool = world.GetPool<TransformData>();

            var buttonFilter = world.Filter<ButtonData>().Inc<TransformData>().End();
            var buttonDataPool = world.GetPool<ButtonData>();
            var buttonTransformDataPool = world.GetPool<TransformData>();

            var gateNeedOpenPool = world.GetPool<GateNeedOpenTag>();

            foreach (var playerEntity in playerFilter)
            {
                ref var playerData = ref playerDataPool.Get(playerEntity);
                ref var playerTransformData = ref playerTransformDataPool.Get(playerEntity);

                foreach (var buttonEntity in buttonFilter)
                {
                    ref var buttonData = ref buttonDataPool.Get(buttonEntity);
                    ref var buttonTransformData = ref buttonTransformDataPool.Get(buttonEntity);

                    var distance = (buttonTransformData.Position - playerTransformData.Position).magnitude;
                    foreach (var packedEntity in buttonData.gateEntities)
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
