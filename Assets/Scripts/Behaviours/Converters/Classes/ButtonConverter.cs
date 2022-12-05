using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Views;
using Leopotam.EcsLite;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FreeTeam.Test.Converters
{
    public class ButtonConverter : MonoBehaviour, IConverter
    {
        #region SerializeFields
        [SerializeField] private ButtonView view = null;
        #endregion

        #region Implementation
        public void Convert(EcsWorld ecsWorld, Dictionary<GameObject, int> entities)
        {
            var entity = ecsWorld.NewEntity();

            var gates = view.Gates
                    .Where(x => x != null)
                    .Select(x => ecsWorld.PackEntity(entities[x]))
                    .Distinct()
                    .ToArray();

            ref var buttonData = ref ecsWorld.GetPool<ButtonData>().Add(entity);
            buttonData.GateEntities = gates;

            ref var transformData = ref ecsWorld.GetPool<TransformData>().Add(entity);
            transformData.Position = view.transform.position;
            transformData.Direction = view.transform.forward;

            entities.Add(view.gameObject, entity);
        }
        #endregion
    }
}
