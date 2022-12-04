using FreeTeam.Test.Behaviours.Providers;
using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Views;
using Leopotam.EcsLite;
using System.Collections.Generic;
using UnityEngine;

namespace FreeTeam.Test.Converters
{
    public class BushConverter : MonoBehaviour, IConverter
    {
        #region SerializeFields
        [SerializeField] private BushView view = null;
        #endregion

        #region Implementation
        public void Convert(EcsWorld ecsWorld, Dictionary<GameObject, int> entities)
        {
            var entity = ecsWorld.NewEntity();

            ecsWorld.GetPool<Bush>().Add(entity);
            ecsWorld.GetPool<ProgressData>().Add(entity).Progress = 0;
            ecsWorld.GetPool<ProviderReference<float>>().Add(entity).Provider = view.GetComponentInChildren<ProgressProvider>();

            entities.Add(view.gameObject, entity);
        }
        #endregion
    }
}
