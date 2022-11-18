using FreeTeam.Test.Common;
using System;
using UnityEngine;

namespace FreeTeam.Test.Behaviours.Providers
{
    public class ProgressProvider : MonoBehaviour, IProvider<float>, IEventProvider
    {
        #region Private
        private float progress = 0f;
        #endregion

        #region Public
        public float Value
        {
            get => progress; 
            set
            {
                if (progress == value) 
                    return;

                progress = value;

                OnChanged();
            }
        }

        public event Action OnChanged = null;
        #endregion
    }
}
