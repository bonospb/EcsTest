using FreeTeam.Test.UI;
using UnityEngine;

namespace FreeTeam.Test.Behaviours
{
    public class SceneContext : MonoBehaviour
    {
        #region SerializeFields
        [Header("Spawnpoints")]
        [SerializeField] private Transform playerSpawnPoint = null;
        [SerializeField] private Transform opponentSpawnPoint = null;
        [Header("Prefabs")]
        [SerializeField] private GameObject playerPrefab = null;
        [SerializeField] private GameObject opponentPrefab = null;
        [Space(10)]
        [SerializeField] private GameObject leftFootprintPrefab = null;
        [SerializeField] private GameObject rightFootprintPrefab = null;
        [Header("Camera")]
        [SerializeField] private Camera mainCamera = null;
        [SerializeField] private GameObject cameraPrefab = null;
        [Header("Containers")]
        [SerializeField] private Transform charactersContainer = null;
        [SerializeField] private Transform footprintContainer = null;
        [Header("UI")]
        [SerializeField] private HUD hud = null;
        #endregion

        #region Public
        public Vector3 PlayerSpawnPointPosition => playerSpawnPoint.position;
        public Quaternion PlayerSpawnPointRotation => playerSpawnPoint.rotation;

        public Vector3 OpponentSpawnPointPosition => opponentSpawnPoint.position;
        public Quaternion OpponentSpawnPointRotation => opponentSpawnPoint.rotation;

        public Camera MainCamera => mainCamera;

        public GameObject PlayerPrefab => playerPrefab;
        public GameObject OpponentPrefab => opponentPrefab;
        public GameObject LeftFootprintPrefab => leftFootprintPrefab;
        public GameObject RightFootprintPrefab => rightFootprintPrefab;
        public GameObject CameraPrefab => cameraPrefab;

        public Transform CharactersContainer => charactersContainer;
        public Transform FootprintContainer => footprintContainer;

        public HUD HUD => hud;
        #endregion
    }
}
