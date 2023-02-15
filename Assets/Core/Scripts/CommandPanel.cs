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
        private GameObject      searchFieldInstance;

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

        public bool IsPageDisable()
        {
            return canvasGroup.alpha == 0;
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
            searchFieldInstance                         =  Instantiate(inputFieldPrefab , transform);
            searchFieldInstance.transform.localPosition += Vector3.up * 360f;
            var searchField              = searchFieldInstance.GetComponent<TMP_InputField>();
            var placeholderTextComponent = searchField.transform.Find("Text Area/Placeholder").GetComponent<TMP_Text>();
            placeholderTextComponent.text = "type command";
        }

        private void Select(GameObject gameObject)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        private void SelectSearchBar()
        {
            Select(searchFieldInstance);
        }

    #endregion
    }
}