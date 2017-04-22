using System.Threading.Tasks;

namespace ZKFM.Core.Services
{
    interface IMusicService<TModel, TResult>
    {

        Task<TModel> GetDetial(int id);
        Task<TResult> Search(string key);
    }
}
