using Leopotam.EcsLite;
using System.Collections.Generic;
using UnityEngine;

namespace FreeTeam.Test.Converters
{
    public interface IConverter
    {
        int Priority { get => 0; }

        void Convert(EcsWorld ecsWorld, Dictionary<GameObject, int> entities);
    }
}
