#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace rStart.UnityCheatTool
{
    public class CheatTool : MonoBehaviour
    {
    #region Public Variables

        public static CheatTool Instance { get; private set; }

    #endregion

    #region Private Variables

        private CanvasGroup canvasGroup;

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
            canvasGroup.alpha = openOnStart ? 1 : 0;
            backdrop.onClick.AddListener(() => SetPageVisible(false));
        }

    #endregion

    #region Public Methods

        public void AddPage<T>() where T : PageBase
        {
            var page = Instantiate(pagePrefab , content);
            page.AddComponent<T>();
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
    }
}