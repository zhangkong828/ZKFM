namespace ZKFM.Core.Services
{
    interface IMusicService<TModel, TResult>
    {

        TModel Get(int id);


        TResult Search(string key);
    }
}
