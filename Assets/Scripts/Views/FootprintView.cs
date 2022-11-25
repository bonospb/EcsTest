using FreeTeam.Test.Behaviours.Providers;
using UnityEngine;


namespace Assets.Scripts.Views
{
    public class FootprintView : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private LifetimeProvider lifetimeProvider = default;
        #endregion

        #region Private
        private float maxValue = 0f;
        #endregion

        #region Unity methods
        private void OnEnable() =>
            lifetimeProvider.OnChanged += OnChangedHandler;

        private void OnDisable() =>
            lifetimeProvider.OnChanged -= OnChangedHandler;
        #endregion

        #region Private methods
        private void OnChangedHandler()
        {
            if (maxValue < lifetimeProvider.Value)
                maxValue = lifetimeProvider.Value;

            //var t = Mathf.InverseLerp(0, maxValue, lifetimeProvider.Value);

            if (lifetimeProvider.Value <= 0)
                Destroy(gameObject);
        }
        #endregion
    }
}