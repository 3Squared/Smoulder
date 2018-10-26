using System.Threading.Tasks;

namespace Smoulder.Interfaces
{
    public interface ISmoulder
    {
        Task Start();
        Task Stop();
    }
}
