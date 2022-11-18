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
            var dt = timeService.Value.FixedDeltaTime;

            timeout -= dt;

            if (timeout > 0)
                return;

            foreach (var entity in filter.Value)
            {
                ref var inputData = ref inputDataPool.Value.Get(entity);
                inputData.TargetPosition = new Vector3(Random.Range(-25, 25), 0f, Random.Range(-25, 25));
            }

            timeout = DELAY;
        }
        #endregion
    }
}
