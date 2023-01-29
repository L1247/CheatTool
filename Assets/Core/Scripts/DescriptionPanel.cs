#region

using TMPro;
using UnityEngine;

#endregion

namespace CheatTool
{
    public class DescriptionPanel : MonoBehaviour
    {
    #region Private Variables

        [SerializeField]
        private TMP_Text descriptionText;

        [SerializeField]
        private CanvasGroup canvasGroup;

    #endregion

    #region Unity events

        private void Awake()
        {
            canvasGroup.alpha    = 0;
            descriptionText.text = string.Empty;
        }

    #endregion

    #region Public Methods

        public void SetDescriptionText(string description)
        {
            descriptionText.text = description;
            canvasGroup.alpha    = string.IsNullOrEmpty(description) ? 0 : 1;
        }

    #endregion
    }
}