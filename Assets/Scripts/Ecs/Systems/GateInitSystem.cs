using FreeTeam.Test.Behaviours;
using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using System.Linq;

namespace FreeTeam.Test.Ecs.Systems
{
    public class GateInitSystem : IEcsInitSystem
    {
        #region Implemetation methods
        public void Init(EcsSystems systems)
        {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            foreach (var links in sharedData.SceneData.ButtonAndGatesLinks)
            {
                var buttonEntity = world.NewEntity();

                var buttonDataPool = world.GetPool<ButtonData>();
                buttonDataPool.Add(buttonEntity);

                var buttonTransformDataPool = world.GetPool<TransformData>();
                buttonTransformDataPool.Add(buttonEntity);

                ref var buttonData = ref buttonDataPool.Get(buttonEntity);
                ref var buttonTransformData = ref buttonTransformDataPool.Get(buttonEntity);

                var entityGatesPairsDic = links.Gates.Where(x => x != null).ToDictionary(e => world.NewEntity(), y => y);

                buttonData.GateEntities = entityGatesPairsDic.Keys.Select(x => world.PackEntity(x)).ToArray();

                buttonTransformData.Position = links.Button.transform.position;
                buttonTransformData.Rotation = links.Button.transform.rotation;

                foreach (var packedEntity in buttonData.GateEntities)
                {
                    if (!packedEntity.Unpack(world, out var gateEntity))
                        continue;

                    var gateDataPool = world.GetPool<GateData>();
                    gateDataPool.Add(gateEntity);

                    var gateTransformDataPool = world.GetPool<TransformData>();
                    gateTransformDataPool.Add(gateEntity);

                    var gateTransformReferencePool = world.GetPool<TransformReference>();
                    gateTransformReferencePool.Add(gateEntity);

                    ref var gateData = ref gateDataPool.Get(gateEntity);
                    ref var gateTransformData = ref gateTransformDataPool.Get(gateEntity);
                    ref var gateTransformReference = ref gateTransformReferencePool.Get(gateEntity);

                    gateData.OpenSpeed = sharedData.Configurations.GateConfig.GateOpenSpeed;

                    gateTransformData.Position = entityGatesPairsDic[gateEntity].transform.position;
                    gateTransformData.Rotation = entityGatesPairsDic[gateEntity].transform.rotation;

                    gateTransformReference.Transform = entityGatesPairsDic[gateEntity].transform;
                }
            }
        }
        #endregion
    }
}
