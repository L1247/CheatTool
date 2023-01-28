#region

using UnityEngine;
using UnityEngine.EventSystems;

#endregion

namespace CheatTool
{
    public class ButtonCellModel : MonoBehaviour , ISelectHandler
    {
    #region Public Methods

        public void OnSelect(BaseEventData eventData)
        {
            Debug.Log($"{gameObject.name}");
        }

    #endregion
    }
}