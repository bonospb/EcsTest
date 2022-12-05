using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class RechargeSystem : IEcsRunSystem
    {
        #region Inject
        private readonly EcsFilterInject<Inc<RechargeData, ProgressData>> filter = default;

        private readonly EcsPoolInject<RechargeData> rechargeDataPool = default;
        private readonly EcsPoolInject<ProgressData> progressDataPool = default;

        private readonly EcsCustomInject<ITimeService> timeService = default;
        #endregion

        #region Implemetation
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var rechargeData = ref rechargeDataPool.Value.Get(entity);
                ref var progressData = ref progressDataPool.Value.Get(entity);

                if (progressData.Progress >= 1)
                    continue;

                var progressTime = Mathf.Lerp(0f, rechargeData.RechargeTime, progressData.Progress);
                progressTime += timeService.Value.DeltaTime;

                progressData.Progress = Mathf.InverseLerp(0, rechargeData.RechargeTime, progressTime);
            }
        }
        #endregion
    }
}
