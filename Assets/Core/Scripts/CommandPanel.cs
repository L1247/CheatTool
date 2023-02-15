#region

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#endregion

namespace rStart.UnityCommandPanel
{
    public class CommandPanel : MonoBehaviour
    {
    #region Public Variables

        public static CommandPanel Instance { get; private set; }

    #endregion

    #region Private Variables

        private CanvasGroup canvasGroup;

        private readonly List<PageBase> pages = new List<PageBase>();

        private PrefabContainer prefabContainer;
        private GameObject      searchBar;

        private TMP_InputField searchField;

        [SerializeField]
        private Button backdrop;

        [SerializeField]
        private GameObject pagePrefab;

        [SerializeField]
        private RectTransform content;

        [SerializeField]
        private bool openOnStart;

        [SerializeField]
        private DescriptionPanel descriptionPanel;

    #endregion

    #region Unity events

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            canvasGroup       = GetComponent<CanvasGroup>();
            prefabContainer   = GetComponent<PrefabContainer>();
            canvasGroup.alpha = openOnStart ? 1 : 0;
            backdrop.onClick.AddListener(() => SetPageVisible(false));
        }

        private void Start()
        {
            AddSearchBar();
            SelectSearchBar();
        }

        private void Update()
        {
            HandleGotoSearchField();
            HandleExecuteButton();
        }

    #endregion

    #region Public Methods

        public PageBase AddOrOpenPage<TPage>() where TPage : PageBase
        {
            var page = pages.Find(page => page.GetType().Name == typeof(TPage).Name);
            if (page == null)
            {
                var pageInstance = Instantiate(pagePrefab , content);
                page = pageInstance.AddComponent<TPage>();
                pages.Add(page);
                page.Init();
            }

            if (pages.Count > 1)
            {
                pages[0].gameObject.SetActive(false);
                pages[1].gameObject.SetActive(true);
            }

            return page;
        }

        public PageBase GetOrCreateInitialPage()
        {
            var alreadyHaveRootPage = pages.Count > 1;
            if (alreadyHaveRootPage) return pages[0];
            var rootPage = AddOrOpenPage<RootPage>();
            return rootPage;
        }

        public void SetDescriptionText(string description)
        {
            descriptionPanel.SetDescriptionText(description);
        }

        public void SetPageVisible(bool visible)
        {
            canvasGroup.alpha = visible ? 1 : 0;
        }

    #endregion

    #region Private Methods

        private void AddSearchBar()
        {
            var inputFieldPrefab = prefabContainer.GetPrefab("InputField");
            searchBar                         =  Instantiate(inputFieldPrefab , transform);
            searchBar.transform.localPosition += Vector3.up * 360f;
            searchField                       =  searchBar.GetComponent<TMP_InputField>();
            var placeholderTextComponent = searchField.transform.Find("Text Area/Placeholder").GetComponent<TMP_Text>();
            placeholderTextComponent.text = "type command";
            searchField.onValueChanged.AddListener(OnSearchFieldChanged);
        }

        private void HandleExecuteButton()
        {
            if (searchField.isFocused == false)
                for (var i = (int)KeyCode.Alpha1 ; i <= (int)KeyCode.Alpha9 ; ++i)
                    if (Input.GetKeyDown((KeyCode)i))
                    {
                        var selectableIndex = i - (int)KeyCode.Alpha1;
                        // ExecuteButtonOfSelectable(selectableIndex);
                    }
        }

        private void HandleGotoSearchField()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Instance.IsPageDisable())
                {
                    SetPageVisible(true);
                    return;
                }

                if (EventSystem.current.currentSelectedGameObject != searchBar) Select(searchBar);
                else SetPageVisible(false);
            }
        }

        private bool IsPageDisable()
        {
            return canvasGroup.alpha == 0;
        }

        private void OnSearchFieldChanged(string str)
        {
            // foreach (var buttonCellModel in buttonCellModels)
            // cellsForSearch.Clear();
            // {
            //     var containKeyword =
            //             buttonCellModel.CellText.Contains(
            //                     str , StringComparison.OrdinalIgnoreCase);
            //     bool active;
            //     if (containKeyword)
            //     {
            //         cellsForSearch.Add(buttonCellModel);
            //         active = true;
            //     }
            //     else
            //     {
            //         active = false;
            //     }
            //
            //     buttonCellModel.gameObject.SetActive(active);
            // }
            //
            // var selectableList = cellsForSearch
            //                     .Select(model => model.Button as UnityEngine.UI.Selectable)
            //                     .ToList();
            // if (selectableList.Count > 0)
            // {
            //     selectableList.Insert(0 , searchField);
            //     SetNavigationOfSelects(selectableList);
            // }
        }

        private void Select(GameObject gameObject)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        private void SelectSearchBar()
        {
            Select(searchBar);
        }

    #endregion
    }
}