using UnityEngine;

namespace Fabros.EcsLite.Behaviours
{
    public class SceneData : MonoBehaviour
    {
        #region Internal
        [System.Serializable]
        public class ButtonAndGatesLink
        {
            public GameObject Button;
            public GameObject[] Gates;
        }
        #endregion

        #region SerializeFields
        [SerializeField] private Transform playerSpawnPoint = null;
        [SerializeField] private Transform opponentSpawnPoint = null;
        [Space(10)]
        [SerializeField] private GameObject playerPrefab = null;
        [SerializeField] private GameObject opponentPrefab = null;
        [Space(10)]
        [SerializeField] private Camera mainCamera = null;
        [SerializeField] private GameObject cameraPrefab = null;
        [Space(10)]
        [SerializeField] private ButtonAndGatesLink[] buttonAndGatesLinks = null;
        #endregion

        #region Public
        public Vector3 PlayerSpawnPointPosition => playerSpawnPoint.position;
        public Quaternion PlayerSpawnPointRotation => playerSpawnPoint.rotation;

        public Vector3 OpponentSpawnPointPosition => opponentSpawnPoint.position;
        public Quaternion OpponentSpawnPointRotation => opponentSpawnPoint.rotation;

        public Camera MainCamera => mainCamera;

        public GameObject PlayerPrefab => playerPrefab;
        public GameObject OpponentPrefab => opponentPrefab;
        public GameObject CameraPrefab => cameraPrefab;

        public ButtonAndGatesLink[] ButtonAndGatesLinks => buttonAndGatesLinks;
        #endregion
    }
}
