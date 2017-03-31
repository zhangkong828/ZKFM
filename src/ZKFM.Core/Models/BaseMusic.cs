using System.Collections.Generic;

namespace ZKFM.Core.Models
{
    public class BaseMusic
    {
        /// <summary>
        /// 歌曲ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 歌曲名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 演唱者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 封面地址
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 歌曲地址
        /// </summary>
        public string Src { get; set; }
        /// <summary>
        /// 歌词
        /// </summary>
        public Lrc Lrc { get; set; }


    }

    public class Lrc
    {
        /// <summary>
        /// 歌词地址
        /// </summary>
        public string Src { get; set; }
        /// <summary>
        /// 歌词文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public KeyValuePair<string, int> Text2 { get; set; }
    }
}
