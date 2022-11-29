using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Ecs.Components.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class GenerateOpponentTargetPointSystem : IEcsRunSystem
    {
        #region Private
        private readonly Range range = new() { Min = -25, Max = 25 };
        #endregion

        #region Inject
        private readonly EcsFilterInject<Inc<Opponent, TransformData>> filter = default;

        private readonly EcsPoolInject<InputTargetPoint> inputTargetPointPool = default;
        private readonly EcsPoolInject<InputDirection> inputDirectionPool = default;
        private readonly EcsPoolInject<TransformData> transformDataPool = default;
        #endregion

        #region Implementation
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

                if (Mathf.Approximately(direction.magnitude, 0f))
                {
                    targetPoint = new Vector3(
                        Random.Range(range.Min, range.Max),
                        0f,
                        Random.Range(range.Min, range.Max));

                    inputTargetPointPool.Value.Replace(entity).TargetPoint = targetPoint;
                }
            }
        }
        #endregion
    }
}
