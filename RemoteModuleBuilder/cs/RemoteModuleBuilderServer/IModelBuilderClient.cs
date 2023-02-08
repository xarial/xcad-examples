using System.Threading.Tasks;

namespace RemoteModuleBuilder
{
    public interface IModelBuilderClient
    {
        Task SendResult(double mass);
        Task UpdateStatus(int totalModelsBuilt);
    }
}
