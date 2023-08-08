using System.Threading.Tasks;

namespace JFrame.UI
{
    public interface IInstantiator<T>
    {
        T Instantiate(string location , T parent);
        Task<T> InstantiateAsync(string goLocation, T parent);
    }

    //public class UnityIns : IInstantiator<Go>
    //{
    //    public Go Instantiate(string location, Go parent)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public Task<Go> InstantiateAsync(string goLocation, Go parent)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}

    //public class Go
    //{

    //}
}