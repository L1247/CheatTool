#region

using System;

#endregion

namespace CheatTool
{
    public static class StringUtils
    {
    #region Public Methods

        public static bool Contains(this string source , string toCheck , StringComparison comp)
        {
            return source.IndexOf(toCheck , comp) >= 0;
        }

    #endregion
    }
}