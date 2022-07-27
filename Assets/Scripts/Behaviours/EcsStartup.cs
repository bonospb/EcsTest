using FreeTeam.Test.Configurations;
using FreeTeam.Test.Ecs.Systems;
using FreeTeam.Test.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace FreeTeam.Test.Behaviours
{
    public class EcsStartup : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private SceneData sceneData = null;
        #endregion

        #region Private
        private EcsWorld world = null;

        private EcsSystems updateSystems = null;
        private EcsSystems fixedUpdateSystem = null;
        #endregion

        #region Unity methods
        private void Start()
        {
            SharedData sharedData = new SharedData(new Configs(), new TimeService(), sceneData);

            world = new EcsWorld();

            updateSystems = new EcsSystems(world, sharedData);
            updateSystems
                .Add(new PlayerInitSystem())
                .Add(new OpponentInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new CameraInitSystem())
                .Add(new GateInitSystem())
                .Add(new TimeSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Init();

            fixedUpdateSystem = new EcsSystems(world, sharedData);
            fixedUpdateSystem
                .Add(new MovementSystem())
                .Add(new RotationSystem())
                .Add(new PushedButtonGateSystem())
                .Add(new GateOpeningSystem())
                .Add(new SetTransformSystem())
                .Add(new GenerateOpponentInputDataSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Init();
        }

        private void Update() =>
            updateSystems?.Run();

        private void FixedUpdate() =>
            fixedUpdateSystem?.Run();

        private void OnDestroy()
        {
            fixedUpdateSystem?.Destroy();
            fixedUpdateSystem = null;

            updateSystems?.Destroy();
            updateSystems = null;

            world?.Destroy();
            world = null;
        }
        #endregion
    }
}
