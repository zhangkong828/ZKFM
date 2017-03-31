using System.Collections.Generic;

namespace ZKFM.Core.Models
{
    public class BaseSearchResult<T> where T : BaseMusic
    {
        public int Total { get; set; }
        public List<T> Datas { get; set; }
    }
}
