namespace JFrame.UI
{
    public class ViewBinder : IViewBinder
    {
        public TScript BindView<TScript, TGameObject>(TGameObject go) where TScript : IView , new()
        {
            var view = new TScript();
            view.Bind(go);
            return view;
        }
    }
}