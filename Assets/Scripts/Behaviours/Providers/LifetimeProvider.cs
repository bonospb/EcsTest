using FreeTeam.Test.Common;
using System;
using UnityEngine;

namespace FreeTeam.Test.Behaviours.Providers
{
    public class LifetimeProvider : MonoBehaviour, IProvider<float>, IEventProvider
    {
        #region Private
        private float lifetime = 0f;
        #endregion

        #region Public
        public float Value
        {
            get => lifetime; 
            set
            {
                if (lifetime == value) 
                    return;

                lifetime = value;

                OnChanged();
            }
        }

        public event Action OnChanged = null;
        #endregion
    }
}
