namespace JFrame.UI
{
    public abstract class ViewBinder : IViewBinder
    {
        public T BindView<T>(IGameObject go) where T : IView
        {
            var view = Create<T>();
            view.Bind(go);
            return view;
        }

        public abstract T Create<T>() where T : IView;
    }
}