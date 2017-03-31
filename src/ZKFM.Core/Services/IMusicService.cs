using System.Threading.Tasks;

namespace ZKFM.Core.Services
{
    interface IMusicService<TModel, TResult>
    {

        Task<TModel> Get(int id);


        Task<TResult> Search(string key);
    }
}
