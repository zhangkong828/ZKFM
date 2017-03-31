using ZKFM.Core.Config;

namespace ZKFM.Core.Models
{
    public class NetEaseMusic : BaseMusic
    {
        public NetEaseMusic()
        {
            MusicType = MusicType.网易云音乐;
        }

        public MusicType MusicType { get; set; }
    }
}
