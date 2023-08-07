using System.Threading.Tasks;

namespace JFrame.UI
{
    public interface IInstantiator<TGameObject>
    {
        TGameObject Instantiate(string location , TGameObject parent);
        Task<TGameObject> InstantiateAsync(string goLocation, TGameObject parent);
    }
}