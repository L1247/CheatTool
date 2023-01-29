#region

using UnityEngine;

#endregion

namespace CheatTool
{
    public class DebugPage : PageBase
    {
    #region Protected Methods

        protected override void Initialization()
        {
            AddButton("Test1" , () => Debug.Log("1"));
            AddButton("Test2" , () => Debug.Log("2"));
            AddButton("Test3" , () => Debug.Log("3"));
            AddButton("Test12" , () => Debug.Log("12"));
            AddButton("Test123" , () => Debug.Log("123"));
            AddButton("Test1234" , () => Debug.Log("1234"));
        }

    #endregion
    }
}