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

        private readonly Color normalOutlineColor   = new Color(0 , 0 , 0 , 0.5f);
        private readonly Color selectedOutlineColor = new Color(0.85f , 0.07f , 0.21f , 1);

    #endregion

    #region Unity events

        private void Awake()
        {
            outline             = GetComponent<Outline>();
            outline.effectColor = normalOutlineColor;
        }

    #endregion

    #region Public Methods

        public void OnDeselect(BaseEventData eventData)
        {
            outline.effectColor    = normalOutlineColor;
            outline.effectDistance = new Vector2(3 , 3);
        }

        public void OnSelect(BaseEventData eventData)
        {
            outline.effectColor    = selectedOutlineColor;
            outline.effectDistance = new Vector2(5 , 5);
        }

    #endregion
    }
}