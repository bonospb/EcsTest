using Fabros.EcsLite.Behaviours;
using Fabros.EcsLite.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Fabros.EcsLite.Ecs.Systems
{
    public class OpponentInitSystem : IEcsInitSystem
    {
        #region Implementation methods
        public void Init(EcsSystems systems)
        {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            var entity = world.NewEntity();

            var opponentPool = world.GetPool<Opponent>();
            opponentPool.Add(entity);

            var movementDataPool = world.GetPool<MovementData>();
            movementDataPool.Add(entity);

            var inputDataPool = world.GetPool<InputData>();
            inputDataPool.Add(entity);

            var transformDataPool = world.GetPool<TransformData>();
            transformDataPool.Add(entity);

            var transformReferencePool = world.GetPool<TransformReference>();
            transformReferencePool.Add(entity);

            GameObject opponentGO = Object.Instantiate(
                sharedData.SceneData.OpponentPrefab,
                sharedData.SceneData.OpponentSpawnPointPosition,
                sharedData.SceneData.OpponentSpawnPointRotation);

            ref var movementData = ref movementDataPool.Get(entity);
            movementData.MoveSpeed = sharedData.Configurations.OpponentConfig.MoveSpeed;
            movementData.RotationSpeed = sharedData.Configurations.OpponentConfig.RotationSpeed;

            ref var transformData = ref transformDataPool.Get(entity);
            transformData.Position = sharedData.SceneData.OpponentSpawnPointPosition;
            transformData.Rotation = sharedData.SceneData.OpponentSpawnPointRotation;

            ref var transformReference = ref transformReferencePool.Get(entity);
            transformReference.Transform = opponentGO.transform;
        }
        #endregion
    }
}
