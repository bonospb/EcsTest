using System;

namespace FreeTeam.Test.Behaviours.Providers
{
    [Flags]
    public enum AnimationTypes
    {
        Idle = 0,
        Walking = 1,
        Gathering = 2,
        Shooting = 4,
        Kick = 8,
    }
}
