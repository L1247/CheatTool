#region

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace DefaultNamespace
{
    public class PageBase : MonoBehaviour
    {
    #region Private Variables

        [SerializeField]
        private Button buttonPrefab;

        [SerializeField]
        private Transform content;

    #endregion

    #region Protected Methods

        protected void AddButton(string cellText , Action clicked = null)
        {
            var button  = Instantiate(buttonPrefab , content);
            var tmpText = button.GetComponentInChildren<TMP_Text>();
            tmpText.text = cellText;
            button.name  = $"Button - {cellText}";
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => clicked?.Invoke());
        }

    #endregion
    }
}