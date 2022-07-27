using FreeTeam.Test.Behaviours;
using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class GateOpeningSystem : IEcsRunSystem
    {
        #region Implemetation methods
        public void Run(EcsSystems systems)
        {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            var filter = world.Filter<IsButtonPushed>().Inc<ButtonData>().End();

            var isButtonPushedPool = world.GetPool<IsButtonPushed>();
            var buttonDataPool = world.GetPool<ButtonData>();
            var gateDataPool = world.GetPool<GateData>();
            var transformDataPool = world.GetPool<TransformData>();

            foreach (var entity in filter)
            {
                ref var buttonData = ref buttonDataPool.Get(entity);

                foreach (var gatePackedEntity in buttonData.GateEntities)
                {
                    if (!gatePackedEntity.Unpack(world, out var gateEntity))
                        continue;

                    ref var gateData = ref gateDataPool.Get(gateEntity);
                    ref var transformData = ref transformDataPool.Get(gateEntity);

                    var dt = sharedData.TimeService.FixedDeltaTime;
                    var speed = gateData.OpenSpeed * dt;

                    transformData.Position += Vector3.down * speed;
                }

                isButtonPushedPool.Del(entity);
            }
        }
        #endregion
    }
}
