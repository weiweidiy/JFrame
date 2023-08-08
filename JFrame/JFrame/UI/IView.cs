namespace JFrame.UI
{

    public interface IView //: IGameObject
    {
        /// <summary>
        /// 绑定游戏对象
        /// </summary>
        /// <typeparam name="TGameObject"></typeparam>
        /// <param name="go"></param>
        void Bind<T>(T go);// where T : IGameObject;

        /// <summary>
        /// 显示
        /// </summary>
        void Show();

        /// <summary>
        /// 关闭
        /// </summary>
        void Hide();

        
    }
}