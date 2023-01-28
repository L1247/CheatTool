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

        private readonly List<UnityEngine.UI.Selectable> selectables = new List<UnityEngine.UI.Selectable>();

        [SerializeField]
        private Button buttonPrefab;

        [SerializeField]
        private TMP_InputField inputFieldPrefab;

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
            buttons.Add(button);

            var tmpText = button.GetComponentInChildren<TMP_Text>();
            tmpText.text = cellText;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => clicked?.Invoke());
            selectables.Add(button);
        }

        protected virtual void Initialization() { }

    #endregion

    #region Private Methods

        private void AddSearchField(string placeholder)
        {
            var inputField               = Instantiate(inputFieldPrefab , content);
            var placeholderTextComponent = inputField.transform.Find("Text Area/Placeholder").GetComponent<TMP_Text>();
            placeholderTextComponent.text = placeholder;
            selectables.Add(inputField);
        }

        private void InitializationAfter()
        {
            var count = selectables.Count;
            for (var index = 0 ; index < count ; index++)
            {
                int upIndex;
                int downIndex;

                var selectableObj = selectables[index];
                selectableObj.gameObject.AddComponent<Selectable>();
                var isFirstCell = index == 0;
                var isLastCell  = index == count - 1;
                if (isFirstCell)
                {
                    upIndex   = count - 1;
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

                var up   = selectables[upIndex];
                var down = selectables[downIndex];

                selectableObj.navigation = new Navigation { mode = Navigation.Mode.Explicit , selectOnUp = up , selectOnDown = down };
            }

            var firstElement = selectables[0].gameObject;
            EventSystem.current.SetSelectedGameObject(firstElement);
        }

    #endregion
    }
}