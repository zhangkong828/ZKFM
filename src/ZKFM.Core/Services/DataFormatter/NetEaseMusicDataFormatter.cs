
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
            var songCount = Regex.Match(json, "\"songCount\":(.+?)\\}").Groups[1].Value;
            int total = 0;
            if (string.IsNullOrEmpty(songCount) || !int.TryParse(songCount, out total) || total <= 0)
            {
                return result;
            }
            result.Total = total;
            //结果集
            var mc = Regex.Matches(Regex.Match(json, "\"songs\":\\[(.+?)\\],\"songCount\"").Groups[0].Value, "\\{\"id\":(.+?),\"name\":\"(.+?)\",\"artists\":\\[\\{\"id\":(.+?),\"name\":\"(.+?)\",[\\s\\S]*?\"rUrl\"");
            if (mc.Count <= 0)
            {
                return result;
            }
            result.Datas = new List<NetEaseMusic>();
            foreach (Match item in mc)
            {
                var id = 0;
                if (int.TryParse(item.Groups[1].Value, out id) && id > 0)
                {
                    result.Datas.Add(new NetEaseMusic()
                    {
                        Id = id,
                        Name = item.Groups[2].Value.Trim(),
                        Author = item.Groups[4].Value.Trim()
                    });
                }
            }
            return result;
        }


        public static NetEaseMusic FormatDetialResult(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;
            var result = new NetEaseMusic();
            var mc = Regex.Matches(json, "\"name\":\"(.+?)\",\"id\":(.+?).+?\"ar\".+?\"id\":(.+?),\"name\":\"(.+?)\".+?\"picUrl\":\"(.+?)\"");
            foreach (Match item in mc)
            {
                var id = 0;
                if (int.TryParse(item.Groups[2].Value, out id) && id > 0)
                {
                    result = new NetEaseMusic()
                    {
                        Id = id,
                        Name = item.Groups[1].Value.Trim(),
                        Author = item.Groups[4].Value.Trim(),
                        Pic = item.Groups[5].Value.Trim()
                    };
                }
                else
                    result = null;
            }
            return result;
        }


        public static Lrc FormatLyricResult(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;
            var result = new Lrc();
            var text = Regex.Match(json, "\"lyric\":\"(.+?)\"\\}").Groups[1].Value;
            if (!string.IsNullOrEmpty(text))
                result.Text = text;
            return result;
        }

        public static string FormatUrlResult(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return null;
            var text = Regex.Match(json, "\"url\":\"(.+?)\"").Groups[1].Value;
            return text;
        }
    }
}
