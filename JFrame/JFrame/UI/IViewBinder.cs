namespace JFrame.UI
{
    /// <summary>
    /// 脚本绑定器, 不同实现要各自实现，比如unity常规的 addcomponent, 或者 lua绑定方式 
    /// </summary>
    /// <typeparam name="TGameObject"></typeparam>
    public interface IViewBinder
    {
        TScript BindView<TScript, TGameObject>(TGameObject go) where TScript : IView , new(); 
    }
}