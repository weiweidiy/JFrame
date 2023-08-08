using System.Threading.Tasks;

namespace JFrame.UI
{
    public interface IUIManager<TGameObject>
    {
        T Open<T>(string goLocation, TGameObject parent) where T : IUIView;

        void Close<T>(T view) where T : IUIView;

        Task<T> OpenAsync<T>(string goLocation, TGameObject parent) where T : IUIView, new();

        void Refresh<T>(T view) where T : IUIView;

        void RefreshAll();

        void CloseAll();


    }
}
