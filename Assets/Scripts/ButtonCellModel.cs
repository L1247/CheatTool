#region

using UnityEngine;
using UnityEngine.UI;

#endregion

namespace CheatTool
{
    public class ButtonCellModel : MonoBehaviour
    {
    #region Public Variables

        public Button Button { get; set; }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                var buttonName = $"Button - {name}";
                Button.name = buttonName;
            }
        }

    #endregion

    #region Private Variables

        private string name;

    #endregion
    }
}