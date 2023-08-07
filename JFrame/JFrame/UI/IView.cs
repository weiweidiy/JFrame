namespace JFrame.UI
{
    public interface IView
    {
        /// <summary>
        /// 获取父节点
        /// </summary>
        IView Parent { get; set; }

        void Bind<TGameObject>(TGameObject go);

        
    }
}