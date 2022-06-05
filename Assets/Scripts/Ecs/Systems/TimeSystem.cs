using Fabros.EcsLite.Behaviours;
using Leopotam.EcsLite;
using UnityEngine;

namespace Fabros.EcsLite.Ecs.Systems
{
    public class TimeSystem : IEcsRunSystem
    {
        #region Implementation methods
        public void Run(EcsSystems systems)
        {
            SharedData shared = systems.GetShared<SharedData>();

            shared.TimeService.DeltaTime = Time.deltaTime;
            shared.TimeService.FixedDeltaTime = Time.fixedDeltaTime;
        }
        #endregion
    }
}
