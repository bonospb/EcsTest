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
    public class GateInitSystem : IEcsInitSystem
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
            foreach (var links in sceneData.Value.ButtonAndGatesLinks)
            {
                var buttonEntity = world.Value.NewEntity();

                ref var buttonData = ref buttonDataPool.Value.Add(buttonEntity);
                ref var buttonTransformData = ref transformDataPool.Value.Add(buttonEntity);

                var entityGatesPairsDic = links.Gates.Where(x => x != null).ToDictionary(e => world.Value.NewEntity(), y => y);

                buttonData.GateEntities = entityGatesPairsDic.Keys.Select(x => world.Value.PackEntity(x)).ToArray();

                buttonTransformData.Position = links.Button.transform.position;
                buttonTransformData.Direction = (links.Button.transform.rotation * Vector3.forward).normalized;
                foreach (var packedEntity in buttonData.GateEntities)
                {
                    if (!packedEntity.Unpack(world.Value, out var gateEntity))
                        continue;

                    ref var gateData = ref gateDataPool.Value.Add(gateEntity);
                    ref var gateProgressData = ref progressDataPool.Value.Add(gateEntity);
                    ref var gateProviderReference = ref providerReferencePool.Value.Add(gateEntity);

                    gateData.OpenSpeed = configs.Value.GateConfig.GateOpenSpeed;
                    gateProgressData.Progress = 0f;
                    gateProviderReference.Provider = entityGatesPairsDic[gateEntity].GetComponentInChildren<ProgressProvider>();
                }
            }
        }
        #endregion
    }
}
