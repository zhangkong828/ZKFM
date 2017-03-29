using System;
using System.Collections.Generic;
using System.Text;

namespace ZKFM.Core.Services
{
    interface IMusicService<TModel, TResult>
    {
        
        TModel Get(int id);

        
        TResult Search(string key);
    }
}
