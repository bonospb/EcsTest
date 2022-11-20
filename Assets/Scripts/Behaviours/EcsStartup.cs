using FreeTeam.Test.Configurations;
using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Ecs.Systems;
using FreeTeam.Test.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace FreeTeam.Test.Behaviours
{
    public class EcsStartup : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private SceneContext sceneData = null;
        #endregion

        #region Private
        private EcsWorld world = null;

        private EcsSystems updateSystems = null;
        private EcsSystems fixedUpdateSystem = null;
        #endregion

        #region Unity methods
        private void Start() =>
            Construct(new Configs(), new TimeService());

        private void Update() =>
            updateSystems?.Run();

        private void FixedUpdate() =>
            fixedUpdateSystem?.Run();

        private void OnDestroy() =>
            Destruct();
        #endregion

        #region Public methods
        public void Construct(IConfigs configs, ITimeService timeService)
        {
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

                .Inject(configs)
                .Inject(timeService)
                .Inject(sceneData)

                .Init();


            fixedUpdateSystem = new EcsSystems(world);
            fixedUpdateSystem
                .Add(new MovementSystem())
                .Add(new RotationSystem())
                .Add(new PushedButtonGateSystem())
                .Add(new GateOpeningSystem())
                .Add(new SetProgressSystem())
                .Add(new SetTransformSystem())
                .Add(new GenerateOpponentInputDataSystem())

#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif

                .DelHere<IsButtonPushed>()

                .Inject(configs)
                .Inject(timeService)
                .Inject(sceneData)

                .Init();
        }

        public void Destruct()
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
