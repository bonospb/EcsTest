using FreeTeam.Test.Configurations;
using FreeTeam.Test.Ecs.Systems;
using FreeTeam.Test.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Behaviours
{
    public class EcsStartup : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private TimeService timeService = null;
        [SerializeField] private SceneData sceneData = null;
        #endregion

        #region Private
        private Configs configs = null;

        private EcsWorld world = null;

        private EcsSystems updateSystems = null;
        private EcsSystems fixedUpdateSystem = null;
        #endregion

        #region Unity methods
        private void Start()
        {
            configs = new Configs();

            world = new EcsWorld();

            updateSystems = new EcsSystems(world);
            updateSystems
                .Add(new PlayerInitSystem())
                .Add(new OpponentInitSystem())
                .Add(new PlayerPointClickInputSystem())
                .Add(new CameraInitSystem())
                .Add(new GateInitSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(configs, timeService, sceneData)
                .Init();

            fixedUpdateSystem = new EcsSystems(world);
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
                .Inject(configs, timeService, sceneData)
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
