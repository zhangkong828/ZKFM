using System;
using System.Collections.Generic;
using System.Text;

namespace ZKFM.Core.Infrastructure
{
    public static class StringExtension
    {
        /// <summary>
        /// 为字符串添加指定样式占位符
        /// </summary>
        /// <param name="s">要修改的字符串</param>
        /// <param name="placeholder">占位符规则</param>
        public static string AddBrackets(this string s, string placeholder = "[{0}]")
        {
            return string.Format(placeholder, s ?? string.Empty);
        }
    }
}
