using FreeTeam.Test.Behaviours;
using FreeTeam.Test.Behaviours.Providers;
using FreeTeam.Test.Configurations;
using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        #region Inject
        private readonly EcsWorldInject world = default;

        private readonly EcsPoolInject<Player> playerPool = default;
        private readonly EcsPoolInject<Unit> unitPool = default;
        private readonly EcsPoolInject<InputData> inputDataPool = default;
        private readonly EcsPoolInject<LastFootprintData> lastFootprintDataPool = default;
        private readonly EcsPoolInject<MovementData> movementDataPool = default;
        private readonly EcsPoolInject<TransformData> transformDataPool = default;
        private readonly EcsPoolInject<TransformReference> transformReferencePool = default;
        private readonly EcsPoolInject<ProviderReference<AnimationTypes>> providerReferencePool = default;

        private readonly EcsCustomInject<IConfigs> configs = default;
        private readonly EcsCustomInject<SceneContext> sceneData = default;
        #endregion

        #region Implementation
        public void Init(IEcsSystems systems)
        {
            var entity = world.Value.NewEntity();

            playerPool.Value.Add(entity);
            unitPool.Value.Add(entity);
            inputDataPool.Value.Add(entity);
            lastFootprintDataPool.Value.Add(entity);

            GameObject playerGO = Object.Instantiate(
                sceneData.Value.PlayerPrefab,
                sceneData.Value.PlayerSpawnPointPosition,
                sceneData.Value.PlayerSpawnPointRotation);

            ref var movementData = ref movementDataPool.Value.Add(entity);
            movementData.MoveSpeed = configs.Value.PlayerConfig.MoveSpeed;
            movementData.RotationSpeed = configs.Value.PlayerConfig.RotationSpeed;

            ref var transformData = ref transformDataPool.Value.Add(entity);
            transformData.Position = sceneData.Value.PlayerSpawnPointPosition;
            transformData.Direction = (sceneData.Value.PlayerSpawnPointRotation * Vector3.forward).normalized;

            ref var transformReference = ref transformReferencePool.Value.Add(entity);
            transformReference.Transform = playerGO.transform;

            ref var providerReference = ref providerReferencePool.Value.Add(entity);
            providerReference.Provider = playerGO.GetComponentInChildren<AnimatorProvider>();
        }
        #endregion
    }
}
