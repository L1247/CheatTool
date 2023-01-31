#region

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#endregion

namespace rStart.UnityCommandPanel
{
    public class PageBase : MonoBehaviour
    {
    #region Private Variables

        private readonly List<ButtonCellModel> buttonCellModels = new List<ButtonCellModel>();
        private readonly List<ButtonCellModel> cellsForSearch   = new List<ButtonCellModel>();

        private readonly List<UnityEngine.UI.Selectable> selectables = new List<UnityEngine.UI.Selectable>();

        private TMP_InputField searchField;

        private CanvasGroup canvasGroup;

        private GameObject buttonPrefab;

        private GameObject inputFieldPrefab;

        private RectTransform content;

        private PrefabContainer prefabContainer;

    #endregion

    #region Unity events

        protected virtual void Start()
        {
            prefabContainer  = GetComponent<PrefabContainer>();
            inputFieldPrefab = prefabContainer.GetPrefab("InputField");
            content          = transform.parent.GetComponent<RectTransform>();
            buttonPrefab     = prefabContainer.GetPrefab("Button");
            AddSearchField("type command");
            Initialization();
            InitializationAfter();
        }

        protected virtual void Update()
        {
            HandleGotoSearchField();
            HandleExecuteButton();
        }

    #endregion

    #region Protected Methods

        protected void AddButton(string cellText , string description = "" , Action clicked = null)
        {
            var button          = Instantiate(buttonPrefab , content).GetComponent<Button>();
            var buttonCellModel = button.gameObject.AddComponent<ButtonCellModel>();
            buttonCellModel.Button      = button;
            buttonCellModel.CellText    = cellText;
            buttonCellModel.Description = description;

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
            var searchFieldInstance = Instantiate(inputFieldPrefab , content);
            searchField = searchFieldInstance.GetComponent<TMP_InputField>();
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

        private void HandleExecuteButton()
        {
            if (searchField.isFocused == false)
                for (var i = (int)KeyCode.Alpha1 ; i <= (int)KeyCode.Alpha9 ; ++i)
                    if (Input.GetKeyDown((KeyCode)i))
                    {
                        var selectableIndex = i - (int)KeyCode.Alpha1;
                        ExecuteButtonOfSelectable(selectableIndex);
                    }
        }

        private void HandleGotoSearchField()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (CommandPanel.Instance.IsPageDisable())
                {
                    SetPageVisible(true);
                    return;
                }

                if (EventSystem.current.currentSelectedGameObject != searchField.gameObject) Select(searchField);
                else SetPageVisible(false);
            }
        }

        private void InitializationAfter()
        {
            SetNavigationOfSelects(selectables);
            SelectFirst();
            searchField.onValueChanged.AddListener(OnSearchFieldChanged);
            foreach (var cellModel in selectables) cellModel.GetComponent<Selectable>().onSelect += OnSelected;
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

        private void OnSelected(RectTransform selectable)
        {
            var description                                                            = string.Empty;
            if (selectable.TryGetComponent(out ButtonCellModel cellModel)) description = cellModel.Description;
            CommandPanel.Instance.SetDescriptionText(description);

            SnapTo(selectable);
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

                var executionNumber = index > 9 ? 0 : index;
                if (selectableObj.TryGetComponent<ButtonCellModel>(out var buttonCellModel))
                    buttonCellModel.SetExecutionNumber(executionNumber);

                var up   = selectableList[upIndex];
                var down = selectableList[downIndex];

                selectableObj.navigation = new Navigation { mode = Navigation.Mode.Explicit , selectOnUp = up , selectOnDown = down };
            }
        }

        private void SetPageVisible(bool visible)
        {
            CommandPanel.Instance.SetPageVisible(visible);
            if (visible) SelectFirst();
        }

        private void SnapTo(RectTransform target)
        {
            var buttonCellModel = target.GetComponent<ButtonCellModel>();
            var findIndex       = cellsForSearch.FindIndex(model => model == buttonCellModel);
            var objHeight       = target.rect.height;

            const int padding              = 100;
            var       localPositionY       = findIndex * objHeight - padding;
            var       contentLocalPosition = new Vector2(content.localPosition.x , localPositionY);

            content.localPosition = contentLocalPosition;
        }

    #endregion
    }
}