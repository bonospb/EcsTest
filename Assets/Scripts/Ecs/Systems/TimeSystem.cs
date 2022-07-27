using FreeTeam.Test.Behaviours;
using Leopotam.EcsLite;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class TimeSystem : IEcsRunSystem
    {
        #region Implementation methods
        public void Run(EcsSystems systems)
        {
            SharedData shared = systems.GetShared<SharedData>();

            shared.TimeService.FixedDeltaTime = Time.fixedDeltaTime;
        }
        #endregion
    }
}
