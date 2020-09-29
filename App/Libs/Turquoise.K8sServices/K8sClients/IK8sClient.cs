using System.Collections.Generic;
using System.Threading.Tasks;

namespace Turquoise.K8sServices.K8sClients
{
    public interface IK8sClient<T>
    {

        IList<T> Get(string nameSpace);
        Task<IList<T>> GetAsync(string nameSpace);
        Task<IList<T>> GetAllAsync();
    }
}