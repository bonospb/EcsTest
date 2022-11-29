using FreeTeam.Test.Behaviours;
using FreeTeam.Test.Behaviours.Providers;
using FreeTeam.Test.Configurations;
using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class OpponentInitSystem : IEcsInitSystem
    {
        #region Inject
        private readonly EcsWorldInject world = default;

        private readonly EcsPoolInject<Opponent> opponentPool = default;
        private readonly EcsPoolInject<Unit> unitPool = default;
        private readonly EcsPoolInject<LastFootprintData> lastFootprintDataPool = default;
        private readonly EcsPoolInject<MovementData> movementDataPool = default;
        private readonly EcsPoolInject<TransformData> transformDataPool = default;
        private readonly EcsPoolInject<TransformReference> transformReferencePool = default;
        private readonly EcsPoolInject<ProviderReference<AnimationTypes>> providerReferencePool = default;

        private readonly EcsCustomInject<IConfigs> configs = default;
        private readonly EcsCustomInject<SceneContext> sceneContext = default;
        #endregion

        #region Implementation
        public void Init(IEcsSystems systems)
        {
            var entity = world.Value.NewEntity();

            opponentPool.Value.Add(entity);
            unitPool.Value.Add(entity);
            lastFootprintDataPool.Value.Add(entity);

            GameObject opponentGO = Object.Instantiate(
                sceneContext.Value.OpponentPrefab,
                sceneContext.Value.CharactersContainer);

            ref var movementData = ref movementDataPool.Value.Add(entity);
            movementData.MoveSpeed = configs.Value.OpponentConfig.MoveSpeed;
            movementData.RotationSpeed = configs.Value.OpponentConfig.RotationSpeed;

            ref var transformData = ref transformDataPool.Value.Add(entity);
            transformData.Position = sceneContext.Value.OpponentSpawnPointPosition;
            transformData.Direction = (sceneContext.Value.OpponentSpawnPointRotation * Vector3.forward).normalized;

            ref var transformReference = ref transformReferencePool.Value.Add(entity);
            transformReference.Transform = opponentGO.transform;

            ref var providerReference = ref providerReferencePool.Value.Add(entity);
            providerReference.Provider = opponentGO.GetComponentInChildren<AnimatorProvider>();
        }
        #endregion
    }
}
