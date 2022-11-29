﻿using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Ecs.Components.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FreeTeam.Test.Ecs.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        #region Inject
        private readonly EcsFilterInject<Inc<Player, TransformData>> filter = default;

        private readonly EcsPoolInject<InputTargetPoint> inputTargetPointPool = default;
        private readonly EcsPoolInject<InputDirection> inputDirectionPool = default;
        private readonly EcsPoolInject<TransformData> transformDataPool = default;
        #endregion

        #region Implemetation
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var transformData = ref transformDataPool.Value.Get(entity);

                var targetPoint = transformData.Position;
                if (inputTargetPointPool.Value.Has(entity))
                    targetPoint = inputTargetPointPool.Value.Get(entity).TargetPoint;

                var direction = (targetPoint - transformData.Position);
                inputDirectionPool.Value.Add(entity).Direction = direction;
            }
        }
        #endregion
    }
}
