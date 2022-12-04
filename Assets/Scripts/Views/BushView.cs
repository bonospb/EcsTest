using FreeTeam.Test.Behaviours.Providers;
using UnityEngine;

namespace FreeTeam.Test.Views
{
    public class BushView : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Transform berries = null;
        [SerializeField] private ProgressProvider progressProvider = default;
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
