using UnityEngine;

namespace FreeTeam.Test.UI
{
    public class HUD : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private Joystick joystick = null;
        #endregion

        #region Public
        public Joystick Joystick => joystick;
        #endregion
    }
}
