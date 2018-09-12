using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ZKFM.Core.Services;

namespace ZKFM.Test
{
    [TestClass]
    public class NetEaseMusicTest
    {
        private NetEaseMusicService _service;

        public NetEaseMusicTest()
        {
            _service = new NetEaseMusicService();
        }

        [TestMethod]
        public void SearchMethod()
        {
            var result = _service.Search("李白", 1, 20).Result;
            Assert.IsNotNull(result);
        }

        
        
        [TestMethod]
        public void DetialMethod()
        {
            var result = _service.GetDetial(27678655).Result;
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void MusicUrlMethod()
        {
            var result = _service.GetMusicUrl(27678655).Result;
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void LyricMethod()
        {
            var result = _service.GetLyric(27678655).Result;
            Assert.IsNotNull(result);
        }
    }
}
