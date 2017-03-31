
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ZKFM.Core.Models;

namespace ZKFM.Core.Services.DataFormatter
{
    public class NetEaseMusicDataFormatter
    {
        public static NetEaseMusicSearchResult FormatSearchResult(string json)
        {
            var result = new NetEaseMusicSearchResult();
            //总数
            if (!Regex.IsMatch(json, "\"code\":200") || Regex.IsMatch(json, "\"songCount\":0"))
            {
                return result;
            }
            var songCount =Regex.Match(json, "\"songCount\":(.+?)\\}").Groups[1].Value;
            //结果集

            return result;
        }
    }
}
