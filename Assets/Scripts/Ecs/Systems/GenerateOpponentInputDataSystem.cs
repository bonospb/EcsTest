using Fabros.EcsLite.Behaviours;
using Fabros.EcsLite.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Fabros.EcsLite.Ecs.Systems
{
    public class GenerateOpponentInputDataSystem : IEcsRunSystem
    {
        private float delay = 3f;
        private float timeout = 0f;

        public void Run(EcsSystems systems)
        {
            SharedData shared = systems.GetShared<SharedData>();

            var dt = shared.TimeService.FixedDeltaTime;

            timeout -= dt;

            if (timeout > 0)
                return;

            var world = systems.GetWorld();

            var filter = world
                .Filter<Opponent>()
                .Inc<InputData>()
                .End();

            var inputDataPool = world.GetPool<InputData>();

            foreach (var entity in filter)
            {
                ref var inputData = ref inputDataPool.Get(entity);

                inputData.TargetPosition = new Vector3(Random.Range(-25, 25), 0f, Random.Range(-25, 25));
            }

            timeout = delay;
        }
    }
}
