using System.Threading.Tasks;

namespace CronWithHost
{
    public interface IWorker
    {
        Task RunAsync();
    }
}