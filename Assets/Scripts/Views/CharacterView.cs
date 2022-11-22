using FreeTeam.Test.Behaviours.Providers;
using UnityEngine;

namespace FreeTeam.Test.Views
{
    public class CharacterView : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Animator animator = null;
        [SerializeField] private AnimatorProvider animatorProvider = null;
        #endregion

        #region Unity methods
        private void OnEnable() =>
            animatorProvider.OnChanged += OnChangedHandler;

        private void OnDisable() =>
            animatorProvider.OnChanged -= OnChangedHandler;
        #endregion

        #region Private methods
        private void OnChangedHandler()
        {
            var animationType = animatorProvider.Value;

            animator.SetBool("isWalking", animationType.HasFlag(AnimationTypes.Walking));
            animator.SetBool("isGathering", animationType.HasFlag(AnimationTypes.Gathering));
        }
        #endregion
    }
}