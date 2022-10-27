using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class SetTransformSystem : IEcsRunSystem
    {
        #region Inject
        private readonly EcsFilterInject<Inc<TransformData, TransformReference>> filter = default;

        private readonly EcsPoolInject<TransformData> transformDataPool = default;
        private readonly EcsPoolInject<TransformReference> transformReferencePool = default;
        #endregion

        #region Implementation
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in filter.Value)
            {
                ref var transformData = ref transformDataPool.Value.Get(entity);
                ref var transformReference = ref transformReferencePool.Value.Get(entity);

                var position = transformData.Position;
                var rotation = Quaternion.LookRotation(transformData.Direction);

                transformReference.Transform.SetPositionAndRotation(position, rotation);
            }
        }
        #endregion
    }
}
