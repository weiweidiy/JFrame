using System.Threading.Tasks;

namespace JFrame.UI
{
    public interface IInstantiator
    {
        IGameObject Instantiate(string location , IGameObject parent);
        Task<IGameObject> InstantiateAsync(string goLocation, IGameObject parent);
    }
}