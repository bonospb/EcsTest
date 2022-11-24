using FreeTeam.Test.Behaviours;
using FreeTeam.Test.Behaviours.Providers;
using FreeTeam.Test.Configurations;
using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Linq;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class ButtonAndGateInitSystem : IEcsInitSystem
    {
        #region Inject
        private readonly EcsWorldInject world = default;

        private readonly EcsPoolInject<GateData> gateDataPool = default;
        private readonly EcsPoolInject<ButtonData> buttonDataPool = default;
        private readonly EcsPoolInject<TransformData> transformDataPool = default;
        private readonly EcsPoolInject<ProgressData> progressDataPool = default;
        private readonly EcsPoolInject<ProviderReference<float>> providerReferencePool = default;

        private readonly EcsCustomInject<IConfigs> configs = default;
        private readonly EcsCustomInject<SceneContext> sceneData = default;
        #endregion

        #region Implemetation
        public void Init(IEcsSystems systems)
        {
            var allGates = sceneData.Value.ButtonAndGatesLinks
                .SelectMany(x => x.Gates)
                .Where(x => x != null)
                .Distinct()
                .ToDictionary(x => x, y => world.Value.NewEntity());

            foreach (var links in sceneData.Value.ButtonAndGatesLinks)
            {
                var buttonEntity = world.Value.NewEntity();

                ref var buttonData = ref buttonDataPool.Value.Add(buttonEntity);
                ref var buttonTransformData = ref transformDataPool.Value.Add(buttonEntity);

                buttonData.GateEntities = links.Gates
                    .Where(x => x != null)
                    .Select(x => world.Value.PackEntity(allGates[x]))
                    .ToArray();

                buttonTransformData.Position = links.Button.transform.position;
                buttonTransformData.Direction = (links.Button.transform.rotation * Vector3.forward).normalized;
            }

            foreach (var gatePair in allGates)
            {
                var gateGameObject = gatePair.Key;
                var gateEntity = gatePair.Value;

                ref var gateData = ref gateDataPool.Value.Add(gateEntity);
                gateData.OpenSpeed = configs.Value.GateConfig.GateOpenSpeed;

                ref var gateProgressData = ref progressDataPool.Value.Add(gateEntity);
                gateProgressData.Progress = 0f;

                ref var gateProviderReference = ref providerReferencePool.Value.Add(gateEntity);
                gateProviderReference.Provider = gateGameObject.GetComponentInChildren<ProgressProvider>();
            }
        }
        #endregion
    }
}
