using System;
using System.Collections.Generic;
using System.Text;

namespace ZKFM.Core.Models
{
    public class BaseSearchResult<T> where T : BaseMusic
    {
        public int Total { get; set; }
        public List<T> Datas { get; set; }
    }
}
