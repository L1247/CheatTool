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

    #endregion

    #region Unity events

        private void Awake()
        {
            descriptionText.text = string.Empty;
        }

    #endregion

    #region Public Methods

        public void SetDescriptionText(string description)
        {
            descriptionText.text = description;
        }

    #endregion
    }
}