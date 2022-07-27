using Cinemachine;
using Fabros.EcsLite.Behaviours;
using Fabros.EcsLite.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Fabros.EcsLite.Ecs.Systems
{
    public class CameraInitSystem : IEcsInitSystem
    {
        #region Implementation methods
        public void Init(EcsSystems systems)
        {
            SharedData sharedData = systems.GetShared<SharedData>();

            EcsWorld world = systems.GetWorld();

            var cameraGO = Object.Instantiate(sharedData.SceneData.CameraPrefab);
            var virtualCameraComp = cameraGO.GetComponent<CinemachineVirtualCamera>();

            var filter = world.Filter<Player>().Inc<TransformReference>().End();

            var transformReferencePool = world.GetPool<TransformReference>();

            foreach (int entity in filter)
            {
                ref var transformReference = ref transformReferencePool.Get(entity);

                virtualCameraComp.Follow = transformReference.Transform;
                virtualCameraComp.LookAt = transformReference.Transform;
            }
        }
        #endregion
    }
}
