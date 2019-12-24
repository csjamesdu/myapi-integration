using System.Threading.Tasks;

namespace myapi.Services
{
    public interface IAppHttpService
    {
        Task<T> GetWithClient<T>(string api, string client);
    }
}
