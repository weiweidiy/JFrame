namespace JFrame.UI
{
    public interface IView
    {
        /// <summary>
        /// 获取父节点
        /// </summary>
        IView Parent { get; set; }

        /// <summary>
        /// 绑定游戏对象
        /// </summary>
        /// <typeparam name="TGameObject"></typeparam>
        /// <param name="go"></param>
        void Bind<TGameObject>(TGameObject go);

        
    }
}