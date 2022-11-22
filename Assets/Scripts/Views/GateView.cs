using FreeTeam.Test.Behaviours.Providers;
using UnityEngine;

namespace FreeTeam.Test.Views
{
    public class GateView : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private ProgressProvider progressProvider = default;

        [SerializeField] private Vector3 openPositionOffset = Vector3.zero;
        #endregion

        #region Private
        private Vector3 closedLocalPosition = Vector3.zero;
        private Vector3 openLocalPosition = Vector3.zero;
        #endregion

        #region Unity methods
        private void Awake()
        {
            closedLocalPosition = transform.localPosition;
            openLocalPosition = closedLocalPosition + openPositionOffset;
        }

        private void OnEnable() =>
            progressProvider.OnChanged += OnChangedHandler;

        private void OnDisable() =>
            progressProvider.OnChanged -= OnChangedHandler;
        #endregion

        #region Private methods
        private void OnChangedHandler() =>
            transform.localPosition = Vector3.Lerp(closedLocalPosition, openLocalPosition, progressProvider.Value);
        #endregion
    }
}
