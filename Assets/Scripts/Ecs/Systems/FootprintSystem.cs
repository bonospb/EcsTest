using FreeTeam.Test.Behaviours;
using FreeTeam.Test.Behaviours.Providers;
using FreeTeam.Test.Configurations;
using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Linq;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class FootprintSystem : IEcsRunSystem
    {
        #region Constants
        private const string UNIT_ID = "unit";
        private const float FOOTPRINT_DELTA = 0.5f;
        #endregion

        #region Inject
        private readonly EcsWorldInject world = default;

        private readonly EcsFilterInject<Inc<Unit, TransformData, LastFootprintData>> filter = default;

        private readonly EcsPoolInject<LastFootprintData> lastFootprintDataPool = default;
        private readonly EcsPoolInject<FootprintData> footprintDataPool = default;
        private readonly EcsPoolInject<TransformData> transformDataPool = default;
        private readonly EcsPoolInject<TransformReference> transformReferencePool = default;
        private readonly EcsPoolInject<LifeTime> lifetimePool = default;
        private readonly EcsPoolInject<ProviderReference<float>> providerReferencePool = default;

        private readonly EcsCustomInject<IConfigs> configs = default;
        private readonly EcsCustomInject<SceneContext> sceneContext = default;
        #endregion

        #region Implementation
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var transformData = ref transformDataPool.Value.Get(entity);
                ref var lastFootprintData = ref lastFootprintDataPool.Value.Get(entity);
                ref var unitTransformData = ref transformDataPool.Value.Get(entity);

                var distance = FOOTPRINT_DELTA;
                var isleft = false;
                if (lastFootprintData.entity.Unpack(world.Value, out var lastFootprintEntity))
                {
                    ref var footprintTransformData = ref transformDataPool.Value.Get(lastFootprintEntity);
                    ref var footprintData = ref footprintDataPool.Value.Get(lastFootprintEntity);

                    distance = Vector3.Distance(unitTransformData.Position, footprintTransformData.Position);
                    isleft = !footprintData.IsLeft;
                }

                if (distance < FOOTPRINT_DELTA)
                    continue;

                lastFootprintData.entity = CreateFootprint(transformData, isleft);
            }
        }
        #endregion

        #region Private methods
        private EcsPackedEntity CreateFootprint(TransformData transformData, bool isLeft = false)
        {
            var footprintPrefab = isLeft
                ? sceneContext.Value.LeftFootprintPrefab
                : sceneContext.Value.RightFootprintPrefab;

            var footprintGO = GameObject.Instantiate(footprintPrefab, sceneContext.Value.FootprintContainer);

            var footprintEntity = world.Value.NewEntity();

            ref var footprintData = ref footprintDataPool.Value.Add(footprintEntity);
            footprintData.IsLeft = isLeft;

            ref var footprintTransformData = ref transformDataPool.Value.Add(footprintEntity);
            footprintTransformData.Position = transformData.Position;
            footprintTransformData.Direction = transformData.Direction;

            ref var transformReference = ref transformReferencePool.Value.Add(footprintEntity);
            transformReference.Transform = footprintGO.transform;

            ref var lifetime = ref lifetimePool.Value.Add(footprintEntity);
            lifetime.Value = configs.Value.FootprintConfigs.FirstOrDefault(x => x.Id == UNIT_ID).Lifetime;

            ref var providerReference = ref providerReferencePool.Value.Add(footprintEntity);
            providerReference.Provider = footprintGO.GetComponentInChildren<LifetimeProvider>();

            return world.Value.PackEntity(footprintEntity);
        }
        #endregion
    }
}
