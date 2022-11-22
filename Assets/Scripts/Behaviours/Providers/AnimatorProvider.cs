using FreeTeam.Test.Common;
using System;
using UnityEngine;

namespace FreeTeam.Test.Behaviours.Providers
{
    public class AnimatorProvider : MonoBehaviour, IProvider<AnimationTypes>, IEventProvider
    {
        #region Private
        private AnimationTypes type = AnimationTypes.Idle;
        #endregion

        #region Public
        public AnimationTypes Value
        {
            get => type;
            set
            {
                if (type.Equals(value))
                    return;

                type = value;

                OnChanged();
            }
        }

        public event Action OnChanged = null;
        #endregion
    }
}