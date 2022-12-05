using FreeTeam.Test.Behaviours.Providers;
using UnityEngine;

namespace FreeTeam.Test.Views
{
    public class BushView : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private ProgressProvider progressProvider = default;
        [Space]
        [SerializeField] private float growthRate = 10f;
        [SerializeField] private Transform berries = null;
        #endregion

        #region Public
        public float GrowthRate => growthRate;
        #endregion

        #region Unity methods
        private void Start() =>
            RefreshState(progressProvider.Value);

        private void OnEnable() =>
            progressProvider.OnChanged += OnProgressChangedHandler;

        private void OnDisable() =>
            progressProvider.OnChanged -= OnProgressChangedHandler;
        #endregion

        #region Private methods
        private void OnProgressChangedHandler() =>
            RefreshState(progressProvider.Value);

        private void RefreshState(float value)
        {
            var isActive = value >= 1;
            berries.gameObject.SetActive(isActive);
        }
        #endregion
    }
}
