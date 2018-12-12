using System.Threading.Tasks;

namespace Smoulder.Interfaces
{
    public interface ISmoulder
    {
        Task Start(params object[] args);
        Task Stop();
    }
}
