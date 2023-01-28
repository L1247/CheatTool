#region

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#endregion

namespace CheatTool
{
    public class PageBase : MonoBehaviour
    {
    #region Private Variables

        private readonly List<ButtonCellModel> buttonCellModels = new List<ButtonCellModel>();
        private readonly List<ButtonCellModel> cellsForSearch   = new List<ButtonCellModel>();

        private readonly List<UnityEngine.UI.Selectable> selectables = new List<UnityEngine.UI.Selectable>();

        private TMP_InputField searchField;

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

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                if (EventSystem.current.currentSelectedGameObject != searchField.gameObject)
                    Select(searchField);
            for (var i = (int)KeyCode.Alpha1 ; i < (int)KeyCode.Alpha9 ; ++i)
                if (Input.GetKeyDown((KeyCode)i))
                {
                    var index = i - (int)KeyCode.Alpha1;
                    ExecuteButtonOfSelectable(index);
                }
        }

    #endregion

    #region Protected Methods

        protected void AddButton(string cellText , Action clicked = null)
        {
            var button          = Instantiate(buttonPrefab , content);
            var buttonCellModel = button.gameObject.AddComponent<ButtonCellModel>();
            buttonCellModel.Button   = button;
            buttonCellModel.CellText = cellText;

            buttonCellModels.Add(buttonCellModel);

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => clicked?.Invoke());
            selectables.Add(button);
            cellsForSearch.Add(buttonCellModel);
        }

        protected virtual void Initialization() { }

    #endregion

    #region Private Methods

        private void AddSearchField(string placeholder)
        {
            searchField = Instantiate(inputFieldPrefab , content);
            var placeholderTextComponent = searchField.transform.Find("Text Area/Placeholder").GetComponent<TMP_Text>();
            placeholderTextComponent.text = placeholder;
            selectables.Add(searchField);
        }

        private void ExecuteButtonOfSelectable(int index)
        {
            if (cellsForSearch.Count <= index) return;
            var selectable = cellsForSearch[index].Button;
            Select(selectable);
            ExecuteEvents.Execute(selectable.gameObject , new BaseEventData(EventSystem.current) , ExecuteEvents.submitHandler);
        }

        private void InitializationAfter()
        {
            SetNavigationOfSelects(selectables);
            SelectFirst();
            searchField.onValueChanged.AddListener(OnSearchFieldChanged);
        }

        private void OnSearchFieldChanged(string str)
        {
            cellsForSearch.Clear();
            foreach (var buttonCellModel in buttonCellModels)
            {
                var containKeyword =
                        buttonCellModel.CellText.Contains(
                                str , StringComparison.OrdinalIgnoreCase);
                bool active;
                if (containKeyword)
                {
                    cellsForSearch.Add(buttonCellModel);
                    active = true;
                }
                else
                {
                    active = false;
                }

                buttonCellModel.gameObject.SetActive(active);
            }

            var selectableList = cellsForSearch
                                .Select(model => model.Button as UnityEngine.UI.Selectable)
                                .ToList();
            if (selectableList.Count > 0)
            {
                selectableList.Insert(0 , searchField);
                SetNavigationOfSelects(selectableList);
            }
        }

        private void Select(UnityEngine.UI.Selectable selectable)
        {
            EventSystem.current.SetSelectedGameObject(selectable.gameObject);
        }

        private void SelectFirst()
        {
            var firstSelectable = selectables[0];
            Select(firstSelectable);
        }

        private void SetNavigationOfSelects(List<UnityEngine.UI.Selectable> selectableList)
        {
            var count = selectableList.Count;
            for (var index = 0 ; index < count ; index++)
            {
                int upIndex;
                int downIndex;

                var selectableObj = selectableList[index];
                if (selectableObj.gameObject.GetComponent<Selectable>() is null) selectableObj.gameObject.AddComponent<Selectable>();
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

                if (selectableObj.TryGetComponent<ButtonCellModel>(out var buttonCellModel)) buttonCellModel.SetExecutionNumber(index);

                var up   = selectableList[upIndex];
                var down = selectableList[downIndex];

                selectableObj.navigation = new Navigation { mode = Navigation.Mode.Explicit , selectOnUp = up , selectOnDown = down };
            }
        }

    #endregion
    }
}