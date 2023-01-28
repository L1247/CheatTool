#region

using UnityEngine;

#endregion

namespace CheatTool
{
    public class DebugPage : PageBase
    {
    #region Unity events

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // foreach (var button in buttons)
                // {
                //     EventSystem.current.SetSelectedGameObject(button.gameObject);
                // }
            }
        }

    #endregion

    #region Protected Methods

        protected override void Initialization()
        {
            AddButton("Test1" , () => Debug.Log("1"));
            AddButton("Test2" , () => Debug.Log("2"));
            AddButton("Test3" , () => Debug.Log("3"));
            AddButton("Test4" , () => Debug.Log("4"));
            AddButton("Test5" , () => Debug.Log("5"));
            AddButton("Test6" , () => Debug.Log("6"));
            AddButton("Test7" , () => Debug.Log("7"));
            AddButton("Test8" , () => Debug.Log("8"));
        }

    #endregion
    }
}