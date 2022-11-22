using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class GateOpeningSystem : IEcsRunSystem
    {
        #region Inject
        private readonly EcsWorldInject world = default;

        private readonly EcsFilterInject<Inc<ButtonData>> filter = default;

        private readonly EcsPoolInject<IsButtonPushed> isButtonPushedPool = default;
        private readonly EcsPoolInject<ButtonData> buttonDataPool = default;
        private readonly EcsPoolInject<GateData> gateDataPool = default;
        private readonly EcsPoolInject<ProgressData> progressDataPool = default;

        private readonly EcsCustomInject<ITimeService> timeService = default;
        #endregion

        #region Private
        private readonly Dictionary<int, bool> openabledGateEntities = new();
        #endregion

        #region Implemetation
        public void Run(IEcsSystems systems)
        {
            openabledGateEntities.Clear();

            foreach (var entity in filter.Value)
            {
                ref var buttonData = ref buttonDataPool.Value.Get(entity);

                foreach (var gatePackedEntity in buttonData.GateEntities)
                {
                    if (!gatePackedEntity.Unpack(world.Value, out var gateEntity))
                        continue;

                    var isOpenabled = isButtonPushedPool.Value.Has(entity);

                    if (!openabledGateEntities.ContainsKey(gateEntity))
                        openabledGateEntities.Add(gateEntity, isOpenabled);
                    else
                        openabledGateEntities[gateEntity] |= isOpenabled;
                }
            }

            foreach (var gateState in openabledGateEntities)
            {
                var gateEntity = gateState.Key;
                var isOpenabled = gateState.Value;

                ref var gateData = ref gateDataPool.Value.Get(gateEntity);
                ref var progressData = ref progressDataPool.Value.Get(gateEntity);

                var dt = timeService.Value.DeltaTime;
                var speed = gateData.OpenSpeed * dt;

                progressData.Progress += isOpenabled ? speed : -speed;
                progressData.Progress = Mathf.Clamp01(progressData.Progress);
            }
        }
        #endregion
    }
}
