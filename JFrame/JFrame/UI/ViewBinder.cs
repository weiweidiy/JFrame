namespace JFrame.UI
{
    /// <summary>
    /// 创建一个View并绑定到指定 gameobject上
    /// </summary>
    public abstract class ViewBinder<TGameObject> : IViewBinder<TGameObject>
    {
        public T BindView<T>(TGameObject go) where T : IView
        {
            var view = Create<T>();
            view.Bind(go);
            return view;
        }

        public abstract T Create<T>() where T : IView;
    }
}