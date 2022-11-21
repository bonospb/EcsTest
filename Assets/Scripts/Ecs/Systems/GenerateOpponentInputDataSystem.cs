using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class GenerateOpponentInputDataSystem : IEcsRunSystem
    {
        #region Constants
        private const float DELAY = 3f;
        #endregion

        #region Private
        private readonly Range range = new() { Min = -25, Max = 25 };
        private float timeout = 0f;
        #endregion

        #region Inject
        private readonly EcsFilterInject<Inc<Opponent, InputData>> filter = default;

        private readonly EcsPoolInject<InputData> inputDataPool = default;

        private readonly EcsCustomInject<ITimeService> timeService = default;
        #endregion

        #region Implementation
        public void Run(IEcsSystems systems)
        {
            var dt = timeService.Value.DeltaTime;

            timeout -= dt;
            if (timeout > 0)
                return;

            foreach (var entity in filter.Value)
            {
                ref var inputData = ref inputDataPool.Value.Get(entity);
                inputData.TargetPosition = new Vector3(
                    Random.Range(range.Min, range.Max), 
                    0f, 
                    Random.Range(range.Min, range.Max));
            }

            timeout = DELAY;
        }
        #endregion
    }
}
