#region

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#endregion

namespace rStart.UnityCommandPanel
{
    public class Selectable : MonoBehaviour , ISelectHandler , IDeselectHandler
    {
    #region Public Variables

        public Action<RectTransform> onSelect;

    #endregion

    #region Private Variables

        private Outline outline;

        private readonly Color normalOutlineColor = new Color(0 , 0 , 0 , 0.5f);

        private readonly Color selectedOutlineColor = new Color(0.85f , 0.07f , 0.21f , 1);

        // private readonly Color         selectedOutlineColor = new Color(.85f,.85f,.85f,1f);
        private RectTransform rectTransform;
        private LayoutElement layoutElement;
        private float         defaultPreferredHeight;
        private float         defaultRectWidth;

    #endregion

    #region Unity events

        private void Awake()
        {
            outline                = GetComponent<Outline>();
            outline.effectColor    = normalOutlineColor;
            rectTransform          = GetComponent<RectTransform>();
            layoutElement          = GetComponent<LayoutElement>();
            defaultPreferredHeight = layoutElement.preferredHeight;
            defaultRectWidth       = rectTransform.rect.width;
        }

    #endregion

    #region Public Methods

        public void OnDeselect(BaseEventData eventData)
        {
            outline.effectColor           = normalOutlineColor;
            outline.effectDistance        = new Vector2(3 , 3);
            layoutElement.preferredHeight = defaultPreferredHeight;
            rectTransform.sizeDelta       = new Vector2(defaultRectWidth , 0);
        }

        public void OnSelect(BaseEventData eventData)
        {
            outline.effectColor    = selectedOutlineColor;
            outline.effectDistance = new Vector2(5 , 5);
            onSelect?.Invoke(rectTransform);
            layoutElement.preferredHeight = defaultPreferredHeight + 30;
            rectTransform.sizeDelta       = new Vector2(defaultRectWidth + 200 , 0);
        }

    #endregion
    }
}