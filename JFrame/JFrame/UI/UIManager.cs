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
    public abstract class UIManager
    {
        /// <summary>
        /// 实例化器
        /// </summary>
        IInstantiator _instantiator;

        /// <summary>
        /// 脚本绑定器
        /// </summary>
        IViewBinder _viewBinder;

        /// <summary>
        /// UIRoot
        /// </summary>
        IGameObject _root;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="instantiator"></param>
        /// <param name="viewBinder"></param>
        public UIManager(IInstantiator instantiator, IViewBinder viewBinder)
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
        public T Open<T>(string goLocation, IGameObject parent) where T : IUIView
        {
            //创建游戏对象
            var go = Instantiate(goLocation, parent);

            //绑定UIView和游戏对象
            var view = BindView<T>(go);

            //设置父子节点关系
            SetRelationship(parent, view);

            return view;
        }

        /// <summary>
        /// 异步打开一个UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="goLocation"></param>
        /// <returns></returns>
        public async Task<T> OpenAsync<T>(string goLocation, IGameObject parent) where T : IUIView, new()
        {
            //创建游戏对象
            var go = await InstantiateAsync(goLocation, parent);
            //绑定UIView和游戏对象
            return BindView<T>(go);
        }


        /// <summary>
        /// 实例化一个可视对象
        /// </summary>
        /// <param name="goLocation"></param>
        /// <returns></returns>
        private IGameObject Instantiate(string goLocation, IGameObject parent)
        {
            return _instantiator.Instantiate(goLocation, parent);
        }

        /// <summary>
        /// 异步实例化
        /// </summary>
        /// <param name="goLocation"></param>
        /// <returns></returns>
        private Task<IGameObject> InstantiateAsync(string goLocation, IGameObject parent)
        {
            return _instantiator.InstantiateAsync(goLocation, parent);
        }


        /// <summary>
        /// 绑定游戏对象和脚本的接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        private T BindView<T>(IGameObject go) where T : IUIView
        {
            return _viewBinder.BindView<T>(go);
        }

        /// <summary>
        /// 设置父子关系
        /// </summary>
        private void SetRelationship(IGameObject parent, IGameObject child)
        {
            child.Parent = parent;
            parent.AddChild(child);
        }
    }
}
