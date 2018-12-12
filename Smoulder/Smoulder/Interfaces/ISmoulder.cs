using System.Threading.Tasks;

namespace Smoulder.Interfaces
{
    public interface ISmoulder
    {
        Task Start(IStartupParameters startupParameters);
        Task Stop();
    }
}
