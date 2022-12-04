using UnityEngine;

namespace FreeTeam.Test.Views
{
    public class ButtonView : MonoBehaviour
    {
        #region SerializeFields
        [SerializeField] private GameObject[] gates = null;
        #endregion

        #region Public
        public GameObject[] Gates => gates;
        #endregion
    }
}
