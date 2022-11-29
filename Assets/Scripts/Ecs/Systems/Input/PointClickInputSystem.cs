﻿using FreeTeam.Test.Behaviours;
using FreeTeam.Test.Ecs.Components;
using FreeTeam.Test.Ecs.Components.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FreeTeam.Test.Ecs.Systems
{
    public class PointClickInputSystem : IEcsRunSystem
    {
        #region Constants
        private const string ACTION_BTN_INPUT_NAME = "Action";
        #endregion

        #region Inject
        private readonly EcsFilterInject<Inc<Player>> filter = default;

        private readonly EcsPoolInject<InputTargetPoint> inputTargetPointPool = default;

        private readonly EcsCustomInject<SceneContext> sceneData = default;
        #endregion

        #region Implemetation
        public void Run(IEcsSystems systems)
        {
            if (Input.GetButtonUp(ACTION_BTN_INPUT_NAME)
                && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                var plane = new Plane(Vector3.up, 0);
                var ray = sceneData.Value.MainCamera.ScreenPointToRay(Input.mousePosition);

                if (plane.Raycast(ray, out var hitDistance))
                {
                    foreach (var entity in filter.Value)
                        inputTargetPointPool.Value.Replace(entity).TargetPoint = ray.GetPoint(hitDistance);
                }
            }
        }
        #endregion
    }
}