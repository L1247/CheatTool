#region

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace DefaultNamespace
{
    public class CheatTool : MonoBehaviour
    {
    #region Public Variables

        public static CheatTool Instance { get; private set; }

    #endregion

    #region Private Variables

        [SerializeField]
        private Transform content;

        [SerializeField]
        private Button buttonPrefab;

    #endregion

    #region Unity events

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }

    #endregion

    #region Public Methods

        public void AddButton(string cellText , Action clicked = null)
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