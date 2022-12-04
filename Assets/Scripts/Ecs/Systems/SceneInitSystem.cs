using FreeTeam.Test.Converters;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class SceneInitSystem : IEcsInitSystem
    {
        #region Inject
        private readonly EcsWorldInject world = default;
        #endregion

        #region Implementation
        public void Init(IEcsSystems systems)
        {
            var viewConverters = GameObject
                .FindObjectsOfType<MonoBehaviour>()
                .OfType<IConverter>()
                .OrderBy(x => x.Priority);

            var entities = new Dictionary<GameObject, int>();
            foreach (var converter in viewConverters)
                converter.Convert(world.Value, entities);
        }
        #endregion
    }
}
