using Fabros.EcsLite.Behaviours;
using Fabros.EcsLite.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Fabros.EcsLite.Ecs.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        #region Implementation methods
        public void Init(EcsSystems systems)
        {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            var playerEntity = world.NewEntity();

            var playerDataPool = world.GetPool<PlayerData>();
            playerDataPool.Add(playerEntity);

            var joysticDataPool = world.GetPool<JoystickData>();
            joysticDataPool.Add(playerEntity);

            var inputDataPool = world.GetPool<InputData>();
            inputDataPool.Add(playerEntity);

            var transformDataPool = world.GetPool<TransformData>();
            transformDataPool.Add(playerEntity);

            var transformReferencePool = world.GetPool<TransformReference>();
            transformReferencePool.Add(playerEntity);

            GameObject playerGO = Object.Instantiate(
                sharedData.SceneData.PlayerPrefab, 
                sharedData.SceneData.PlayerSpawnPointPosition,
                sharedData.SceneData.PlayerSpawnPointRotation);

            ref var playerData = ref playerDataPool.Get(playerEntity);
            playerData.MoveSpeed = sharedData.Configurations.PlayerConfig.PlayerMoveSpeed;
            playerData.RotationSpeed = sharedData.Configurations.PlayerConfig.PlayerRotationSpeed;

            ref var transformData = ref transformDataPool.Get(playerEntity);
            transformData.Position = sharedData.SceneData.PlayerSpawnPointPosition;
            transformData.Rotation = sharedData.SceneData.PlayerSpawnPointRotation;

            ref var transformReference = ref transformReferencePool.Get(playerEntity);
            transformReference.Transform = playerGO.transform;
        }
        #endregion
    }
}
