using FreeTeam.Test.Common;
using UnityEngine;

namespace FreeTeam.Test.Services
{
    public class TimeService : MonoBehaviour, ITimeService
    {
        #region Implementation
        public float DeltaTime => Time.deltaTime;

        public float FixedDeltaTime => Time.fixedDeltaTime;
        #endregion
    }
}
