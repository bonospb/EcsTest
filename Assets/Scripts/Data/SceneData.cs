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
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private GameObject playerPrefab;
        [Space(10)]
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameObject cameraPrefab;
        [Space(10)]
        [SerializeField] private ButtonAndGatesLink[] buttonAndGatesLinks;
        #endregion

        #region Public
        public Vector3 PlayerSpawnPointPosition => playerSpawnPoint.position;
        public Quaternion PlayerSpawnPointRotation => playerSpawnPoint.rotation;

        public Camera MainCamera => mainCamera;

        public GameObject PlayerPrefab => playerPrefab;
        public GameObject CameraPrefab => cameraPrefab;

        public ButtonAndGatesLink[] ButtonAndGatesLinks => buttonAndGatesLinks;

        public FixedJoystick joystick;
        #endregion
    }
}
