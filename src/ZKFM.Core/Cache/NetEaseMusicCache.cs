using System;
using System.Collections.Generic;
using System.Text;
using ZKFM.Core.Config;
using ZKFM.Core.Models;

namespace ZKFM.Core.Cache
{
    public class NetEaseMusicCache : BaseCache<NetEaseMusic>
    {
        public NetEaseMusicCache()
        {
            ThresholdCount = 1000;
            ExpireTime = 60;
        }
    }
}
