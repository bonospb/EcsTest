using Cinemachine;
using FreeTeam.Test.Behaviours;
using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class CameraInitSystem : IEcsInitSystem
    {
        #region Inject
        private readonly EcsFilterInject<Inc<Player, TransformReference>> filter = default;

        private readonly EcsPoolInject<TransformReference> transformReferencePool = default;

        private readonly EcsCustomInject<SceneData> sceneData = default;
        #endregion

        #region Implementation
        public void Init(IEcsSystems systems)
        {
            var cameraGO = Object.Instantiate(sceneData.Value.CameraPrefab);
            var virtualCameraComp = cameraGO.GetComponent<CinemachineVirtualCamera>();

            foreach (int entity in filter.Value)
            {
                ref var transformReference = ref transformReferencePool.Value.Get(entity);

                virtualCameraComp.Follow = transformReference.Transform;
                virtualCameraComp.LookAt = transformReference.Transform;
            }
        }
        #endregion
    }
}
