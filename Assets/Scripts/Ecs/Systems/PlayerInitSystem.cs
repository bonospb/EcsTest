using FreeTeam.Test.Behaviours;
using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        #region Implementation methods
        public void Init(EcsSystems systems)
        {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            var entity = world.NewEntity();

            var playerPool = world.GetPool<Player>();
            playerPool.Add(entity);

            var movementDataPool = world.GetPool<MovementData>();
            movementDataPool.Add(entity);

            var inputDataPool = world.GetPool<InputData>();
            inputDataPool.Add(entity);

            var transformDataPool = world.GetPool<TransformData>();
            transformDataPool.Add(entity);

            var transformReferencePool = world.GetPool<TransformReference>();
            transformReferencePool.Add(entity);

            GameObject playerGO = Object.Instantiate(
                sharedData.SceneData.PlayerPrefab,
                sharedData.SceneData.PlayerSpawnPointPosition,
                sharedData.SceneData.PlayerSpawnPointRotation);

            ref var movementData = ref movementDataPool.Get(entity);
            movementData.MoveSpeed = sharedData.Configurations.PlayerConfig.MoveSpeed;
            movementData.RotationSpeed = sharedData.Configurations.PlayerConfig.RotationSpeed;

            ref var transformData = ref transformDataPool.Get(entity);
            transformData.Position = sharedData.SceneData.PlayerSpawnPointPosition;
            transformData.Rotation = sharedData.SceneData.PlayerSpawnPointRotation;

            ref var transformReference = ref transformReferencePool.Get(entity);
            transformReference.Transform = playerGO.transform;
        }
        #endregion
    }
}
