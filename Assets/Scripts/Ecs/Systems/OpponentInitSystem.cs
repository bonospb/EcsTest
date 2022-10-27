using FreeTeam.Test.Behaviours;
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
        private readonly EcsPoolInject<MovementData> movementDataPool = default;
        private readonly EcsPoolInject<InputData> inputDataPool = default;
        private readonly EcsPoolInject<TransformData> transformDataPool = default;
        private readonly EcsPoolInject<TransformReference> transformReferencePool = default;

        private readonly EcsCustomInject<Configs> configs = default;
        private readonly EcsCustomInject<SceneData> sceneData = default;
        #endregion

        #region Implementation
        public void Init(IEcsSystems systems)
        {
            var entity = world.Value.NewEntity();

            opponentPool.Value.Add(entity);
            inputDataPool.Value.Add(entity);

            GameObject opponentGO = Object.Instantiate(
                sceneData.Value.OpponentPrefab,
                sceneData.Value.OpponentSpawnPointPosition,
                sceneData.Value.OpponentSpawnPointRotation);

            ref var movementData = ref movementDataPool.Value.Add(entity);
            movementData.MoveSpeed = configs.Value.OpponentConfig.MoveSpeed;
            movementData.RotationSpeed = configs.Value.OpponentConfig.RotationSpeed;

            ref var transformData = ref transformDataPool.Value.Add(entity);
            transformData.Position = sceneData.Value.OpponentSpawnPointPosition;
            transformData.Direction = (sceneData.Value.OpponentSpawnPointRotation * Vector3.forward).normalized;

            ref var transformReference = ref transformReferencePool.Value.Add(entity);
            transformReference.Transform = opponentGO.transform;
        }
        #endregion
    }
}
