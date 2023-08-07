using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JFrame.UI
{
    /// <summary>
    /// UI管理器
    /// </summary>
    /// <typeparam name="TGameObject">游戏对象类型，例：Unity的就是 GameObject </typeparam>
    public abstract class UIManager<TGameObject>
    {
        /// <summary>
        /// 实例化器
        /// </summary>
        IInstantiator<TGameObject> _instantiator;

        /// <summary>
        /// 脚本绑定器
        /// </summary>
        IViewBinder _viewBinder;

        /// <summary>
        /// UIRoot
        /// </summary>
        TGameObject _root;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="instantiator"></param>
        /// <param name="viewBinder"></param>
        public UIManager(IInstantiator<TGameObject> instantiator, IViewBinder viewBinder)
        {
            _instantiator = instantiator;
            _viewBinder = viewBinder;
        }

        /// <summary>
        /// 打开一个指定预制体的UI，并返回view，挂载在root节点上
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="goLocation"></param>
        /// <returns></returns>
        public TView Open<TView>(string goLocation, TGameObject parent) where TView : IUIView, new()
        {
            //创建游戏对象
            var go = Instantiate(goLocation, parent);

            //绑定UIView和游戏对象
            return BindView<TView>(go);
        }

        /// <summary>
        /// 异步打开一个UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="goLocation"></param>
        /// <returns></returns>
        public async Task<TView> OpenAsync<TView>(string goLocation, TGameObject parent) where TView : IUIView, new()
        {
            //创建游戏对象
            var go = await InstantiateAsync(goLocation, parent);
            //绑定UIView和游戏对象
            return BindView<TView>(go);
        }

        /// <summary>
        /// 绑定游戏对象和脚本的接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        private TView BindView<TView>(TGameObject go) where TView : IUIView ,new()
        {
            return _viewBinder.BindView<TView, TGameObject>(go);
        }

        /// <summary>
        /// 实例化一个可视对象
        /// </summary>
        /// <param name="goLocation"></param>
        /// <returns></returns>
        private TGameObject Instantiate(string goLocation, TGameObject parent)
        {
            return _instantiator.Instantiate(goLocation, parent);
        }

        /// <summary>
        /// 异步实例化
        /// </summary>
        /// <param name="goLocation"></param>
        /// <returns></returns>
        private Task<TGameObject> InstantiateAsync(string goLocation, TGameObject parent)
        {
            return _instantiator.InstantiateAsync(goLocation, parent);
        }
    }
}
