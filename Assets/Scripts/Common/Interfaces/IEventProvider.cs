using System;

namespace FreeTeam.Test.Common
{
    public interface IEventProvider
    {
        event Action OnChanged;
    }
}
