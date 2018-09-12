using System;
using System.Collections.Generic;
using System.Text;
using ZKFM.Core.Models;

namespace ZKFM.Core.Cache
{
    public class NetEaseMusicSearchResultCache : BaseCache<NetEaseMusicSearchResult>
    {
        public NetEaseMusicSearchResultCache()
        {
            ThresholdCount = 1000;
            ExpireTime = 60;
        }
    }
}
