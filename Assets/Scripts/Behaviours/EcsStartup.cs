using Fabros.EcsLite.Configurations;
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
        private EcsWorld ecsWorld;

        private EcsSystems updateSystems;
        private EcsSystems fixedUpdateSystem;
        #endregion

        #region Unity methods
        private void Start()
        {
            SharedData sharedData = new SharedData(new RuntimeData(), new Configs(), sceneData);

            ecsWorld = new EcsWorld();

            updateSystems = new EcsSystems(ecsWorld, sharedData);
            updateSystems
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Init();

            fixedUpdateSystem = new EcsSystems(ecsWorld, sharedData);
            fixedUpdateSystem
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

            ecsWorld?.Destroy();
            ecsWorld = null;
        }
        #endregion
    }
}
