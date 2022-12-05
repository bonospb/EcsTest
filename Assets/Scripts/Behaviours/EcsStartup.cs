using FreeTeam.Test.Configurations;
using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Ecs.Components.Input;
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
        private IConfigs configs = null;
        private ITimeService timeService = null;

        private EcsWorld world = null;

        private EcsSystems initSystems = null;
        private EcsSystems updateSystem = null;
        #endregion

        #region Unity methods
        private void Start()
        {
            if (!isLocalRun)
                return;

            Construct(new EcsWorld(), new Configs(), new TimeService());
        }

        private void Update() =>
            updateSystem?.Run();

        private void OnDestroy() =>
            Destruct();
        #endregion

        #region Public methods
        public void Construct(EcsWorld world, IConfigs configs, ITimeService timeService)
        {
            this.world = world;
            this.configs = configs;
            this.timeService = timeService;

            BuildSystems();
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

        #region Private
        private void BuildSystems()
        {
            initSystems = new EcsSystems(world);
            initSystems
                .Add(new SceneInitSystem())

                .Add(new PlayerInitSystem())
                .Add(new OpponentInitSystem())

                .Add(new CameraInitSystem())

                .Inject(configs)
                .Inject(timeService)
                .Inject(sceneContext)

                .Init();


            updateSystem = new EcsSystems(world);
            updateSystem
                .Add(new PointClickInputSystem())
                .Add(new KeyboardInputSystem())
                .Add(new JoystickInputSystem())

                .Add(new PlayerInputSystem())
                .Add(new GenerateOpponentTargetPointSystem())

                .Add(new AnimateCharacterSystem())

                .Add(new MovementSystem())
                .Add(new RotationSystem())

                .Add(new FootprintSystem())

                .Add(new PushedButtonGateSystem())
                .Add(new GateOpeningSystem())

                .Add(new RechargeSystem())
                .Add(new SetProgressSystem())

                .Add(new SetTransformSystem())
                .Add(new AnimateCharacterSystem())

                .Add(new EntitiesLifeTimeSystem())
                .Add(new DestroyEntitiesSystem())

#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif

                .DelHere<InputDirection>()
                .DelHere<InputAction>()
                .DelHere<InputAttack>()
                .DelHere<IsMoving>()
                .DelHere<IsButtonPushed>()

                .Inject(configs)
                .Inject(timeService)
                .Inject(sceneContext)

                .Init();
        }
        #endregion
    }
}
