#region

using UnityEngine;

#endregion

namespace rStart.UnityCheatTool
{
    public class DebugPage : PageBase
    {
    #region Protected Methods

        protected override void Initialization()
        {
            AddButton("Test1" , "<color=red>測試</color>" , () => Debug.Log("1"));
            AddButton("Test2" , "<color=green>測試kdfhjsklhdkl</color>" , () => Debug.Log("2"));
            AddButton("Test3" , "<color=lightblue>我是說明文字\n我是說明文字</color>" , () => Debug.Log("3"));
            AddButton("Test4" , clicked : () => Debug.Log("12"));
            AddButton("Test5" , clicked : () => Debug.Log("12"));
            AddButton("Test6" , clicked : () => Debug.Log("12"));
            AddButton("Test7" , clicked : () => Debug.Log("12"));
            AddButton("Test8" , clicked : () => Debug.Log("12"));
            AddButton("Test9" , clicked : () => Debug.Log("12"));
            AddButton("Test12" , clicked : () => Debug.Log("12"));
            AddButton("Test123" , clicked : () => Debug.Log("123"));
            AddButton("Test1234" , clicked : () => Debug.Log("1234"));
        }

    #endregion
    }
}