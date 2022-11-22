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
        [SerializeField] private bool isLocalRun = true;
        [SerializeField] private SceneContext sceneContext = null;
        #endregion

        #region Private
        private EcsWorld world = null;

        private EcsSystems initSystems = null;
        private EcsSystems updateSystem = null;
        #endregion

        #region Unity methods
        private void Start()
        {
            if (!isLocalRun)
                return;

            Construct(new Configs(), new TimeService());
        }

        private void Update() =>
            updateSystem?.Run();

        private void OnDestroy() =>
            Destruct();
        #endregion

        #region Public methods
        public void Construct(IConfigs configs, ITimeService timeService)
        {
            world = new EcsWorld();

            initSystems = new EcsSystems(world);
            initSystems
                .Add(new PlayerInitSystem())
                .Add(new OpponentInitSystem())
                .Add(new CameraInitSystem())
                .Add(new GateInitSystem())

                .Inject(configs)
                .Inject(timeService)
                .Inject(sceneContext)

                .Init();


            updateSystem = new EcsSystems(world);
            updateSystem
                .Add(new PlayerPointClickInputSystem())
                .Add(new GenerateOpponentInputDataSystem())

                .Add(new AnimateCharacterSystem())

                .Add(new MovementSystem())
                .Add(new RotationSystem())

                .Add(new PushedButtonGateSystem())
                .Add(new GateOpeningSystem())

                .Add(new SetProgressSystem())

                .Add(new SetTransformSystem())
                .Add(new AnimateCharacterSystem())

#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif

                .DelHere<IsMoving>()
                .DelHere<IsButtonPushed>()

                .Inject(configs)
                .Inject(timeService)
                .Inject(sceneContext)

                .Init();
        }

        public void Destruct()
        {
            updateSystem?.Destroy();
            updateSystem = null;

            initSystems?.Destroy();
            initSystems = null;

            world?.Destroy();
            world = null;
        }
        #endregion
    }
}
