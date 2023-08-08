namespace JFrame.UI
{
    /// <summary>
    /// 脚本绑定器, 不同实现要各自实现，比如unity常规的 addcomponent, 或者 lua绑定方式 
    /// </summary>
    /// <typeparam name="TGameObject"></typeparam>
    public interface IViewBinder<TGameObject>
    {
        /// <summary>
        /// 绑定游戏对象和脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        T BindView<T>(TGameObject go) where T : IView;

        /// <summary>
        /// 创建脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Create<T>() where T : IView;
    }
}