using UnityEngine;

namespace Fabros.EcsLite.Behaviours
{
    public class SceneData : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private GameObject playerPrefab;
        [Space(10)]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameObject cameraPrefab;
        #endregion

        #region Public
        public Vector3 PlayerSpawnPointPosition => playerSpawnPoint.position;
        public Quaternion PlayerSpawnPointRotation => playerSpawnPoint.rotation;

        public Camera MainCamera => mainCamera;

        public GameObject PlayerPrefab => playerPrefab;
        public GameObject CameraPrefab => cameraPrefab;
        #endregion
    }
}
