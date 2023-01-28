#region

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#endregion

namespace CheatTool
{
    public class PageBase : MonoBehaviour
    {
    #region Protected Variables

        protected readonly List<Button> buttons = new List<Button>();

    #endregion

    #region Private Variables

        [SerializeField]
        private Button buttonPrefab;

        [SerializeField]
        private Transform content;

    #endregion

    #region Unity events

        protected virtual void Start()
        {
            AddSearchField("type something");
            Initialization();
            InitializationAfter();
        }

    #endregion

    #region Protected Methods

        protected void AddButton(string cellText , Action clicked = null)
        {
            var button = Instantiate(buttonPrefab , content);
            button.name = $"Button - {cellText}";
            button.gameObject.AddComponent<ButtonCellModel>();
            buttons.Add(button);
            if (buttons.Count == 1) EventSystem.current.SetSelectedGameObject(button.gameObject);

            var tmpText = button.GetComponentInChildren<TMP_Text>();
            tmpText.text = cellText;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => clicked?.Invoke());
        }

        protected void AddSearchField(string placeholder) { }

        protected virtual void Initialization() { }

    #endregion

    #region Private Methods

        private void InitializationAfter()
        {
            var buttonCount = buttons.Count;
            for (var index = 0 ; index < buttonCount ; index++)
            {
                int upIndex;
                int downIndex;

                var button      = buttons[index];
                var isFirstCell = index == 0;
                var isLastCell  = index == buttonCount - 1;
                if (isFirstCell)
                {
                    upIndex   = buttonCount - 1;
                    downIndex = index + 1;
                }
                else if (isLastCell)
                {
                    upIndex   = index - 1;
                    downIndex = 0;
                }
                else
                {
                    upIndex   = index - 1;
                    downIndex = index + 1;
                }

                var up   = buttons[upIndex];
                var down = buttons[downIndex];

                button.navigation = new Navigation { mode = Navigation.Mode.Explicit , selectOnUp = up , selectOnDown = down };
            }
        }

    #endregion
    }
}