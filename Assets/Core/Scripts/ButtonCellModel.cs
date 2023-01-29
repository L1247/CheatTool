#region

using TMPro;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace rStart.UnityCheatTool
{
    public class ButtonCellModel : MonoBehaviour
    {
    #region Public Variables

        public Button Button
        {
            get => button;
            set
            {
                button              = value;
                executionNumberText = button.transform.Find("HorizontalLayoutGroup/Execution Number").GetComponent<TMP_Text>();
            }
        }
        public string CellText
        {
            get => cellText;
            set
            {
                cellText = value;
                var buttonName = $"Button - {cellText}";
                Button.name = buttonName;
                var tmpText = button.transform.Find("HorizontalLayoutGroup/CellText").GetComponent<TMP_Text>();
                tmpText.text = value;
            }
        }
        public string Description { get; set; }

    #endregion

    #region Private Variables

        private string cellText;

        private TMP_Text executionNumberText;

        [SerializeField]
        private Button button;

    #endregion

    #region Public Methods

        public void SetExecutionNumber(int number)
        {
            executionNumberText.text = number.ToString();
        }

    #endregion
    }
}