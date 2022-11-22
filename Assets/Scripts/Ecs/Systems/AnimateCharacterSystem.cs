using FreeTeam.Test.Behaviours.Providers;
using FreeTeam.Test.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FreeTeam.Test.Ecs.Systems
{
    public class AnimateCharacterSystem : IEcsRunSystem
    {
        #region Inject
        private readonly EcsWorldInject world = default;

        private readonly EcsFilterInject<Inc<MovementData>> movementFilter = default;

        private readonly EcsPoolInject<IsMoving> isMovingPool = default;
        private readonly EcsPoolInject<ProviderReference<AnimationTypes>> animationTypeProviderPool = default;
        #endregion

        #region Implementation
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in movementFilter.Value)
            {
                if (!animationTypeProviderPool.Value.Has(entity))
                    continue;

                ref var animationTypeProvider = ref animationTypeProviderPool.Value.Get(entity);

                if (isMovingPool.Value.Has(entity))
                {
                    if (!animationTypeProvider.Provider.Value.HasFlag(AnimationTypes.Walking))
                        animationTypeProvider.Provider.Value |= AnimationTypes.Walking;
                }
                else
                {
                    if (animationTypeProvider.Provider.Value.HasFlag(AnimationTypes.Walking))
                        animationTypeProvider.Provider.Value &= ~AnimationTypes.Walking;
                }
            }
        }
        #endregion
    }
}
