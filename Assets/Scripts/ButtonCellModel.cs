#region

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#endregion

namespace CheatTool
{
    public class ButtonCellModel : MonoBehaviour , ISelectHandler , IDeselectHandler
    {
    #region Private Variables

        private Outline outline;

    #endregion

    #region Unity events

        private void Awake()
        {
            outline = GetComponent<Outline>();
        }

    #endregion

    #region Public Methods

        public void OnDeselect(BaseEventData eventData)
        {
            outline.effectColor = Color.black;
        }

        public void OnSelect(BaseEventData eventData)
        {
            outline.effectColor = new Color(0.85f , 0.07f , 0.21f , 1);
        }

    #endregion
    }
}