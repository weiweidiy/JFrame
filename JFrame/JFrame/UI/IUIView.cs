namespace JFrame.UI
{
    public interface IUIView : IView
    {

    }

    public class UIView : IUIView
    {
        public IView Parent { get ; set ; }

        public void Bind<TGameObject>(TGameObject go)
        {
            
        }
    }
}