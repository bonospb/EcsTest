using Fabros.EcsLite.Configurations;
using Fabros.EcsLite.Ecs.Systems;
using Fabros.EcsLite.Services;
using Leopotam.EcsLite;
using UnityEngine;

namespace Fabros.EcsLite.Behaviours
{
    public class EcsStartup : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private PlayerConfig configuration;
        [SerializeField] private SceneData sceneData;
        #endregion

        #region Private
        private EcsWorld world;
        private EcsSystems updateSystems;
        private EcsSystems fixedUpdateSystem;
        #endregion

        #region Unity methods
        private void Start()
        {
            SharedData sharedData = new SharedData(new Configs(), new TimeService(), sceneData);

            world = new EcsWorld();

            updateSystems = new EcsSystems(world, sharedData);
            updateSystems
                .Add(new PlayerInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new CameraInitSystem())
                .Add(new TimeSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Init();

            fixedUpdateSystem = new EcsSystems(world, sharedData);
            fixedUpdateSystem
                .Add(new MovementSystem())
                .Add(new RotationSystem())
                .Add(new SetTransformSystem())
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
