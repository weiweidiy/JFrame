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
    public abstract class UIManager<TGameObject> : IUIManager<TGameObject> , IDisposable
    {
        /// <summary>
        /// 实例化器
        /// </summary>
        IInstantiator<TGameObject> instantiator;

        /// <summary>
        /// 脚本绑定器
        /// </summary>
        IViewBinder<TGameObject> viewBinder;

        /// <summary>
        /// UIRoot
        /// </summary>
        protected TGameObject root;

        /// <summary>
        /// 缓存的所有的UI
        /// </summary>
        Dictionary<Type, List<IUIView>> uiViews = new Dictionary<Type, List<IUIView>>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="instantiator"></param>
        /// <param name="viewBinder"></param>
        public UIManager(IInstantiator<TGameObject> instantiator, IViewBinder<TGameObject> viewBinder)
        {
            this.instantiator = instantiator;
            this.viewBinder = viewBinder;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            root = default(TGameObject);
            instantiator = null;
            viewBinder = null;
            uiViews.Clear();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="root"></param>
        public void SetRoot(TGameObject root)
        {
            this.root = root;
        }

        /// <summary>
        /// 打开一个指定预制体的UI，并返回view，挂载在root节点上
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="goLocation"></param>
        /// <returns></returns>
        public T Open<T>(string goLocation, TGameObject parent) where T : IUIView
        {
            //判断该类型UI是否可以被打开


            //创建游戏对象
            var go = Instantiate(goLocation, parent);

            //绑定UIView和游戏对象
            var view = BindView<T>(go);

            //设置父子节点关系
            SetRelationship(parent, view);

            //添加到缓存字典
            AddToCache(view);

            return view;
        }



        /// <summary>
        /// 异步打开一个UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="goLocation"></param>
        /// <returns></returns>
        public async Task<T> OpenAsync<T>(string goLocation, TGameObject parent) where T : IUIView, new()
        {
            //创建游戏对象
            var go = await InstantiateAsync(goLocation, parent);
            //绑定UIView和游戏对象
            return BindView<T>(go);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        public void Close<T>(T view) where T : IUIView
        {
            //销毁GameObject
        }

        #region 抽象方法
        /// <summary>
        /// 设置父子关系,子类实现
        /// </summary>
        protected abstract void SetRelationship(TGameObject parent, IUIView child);

        #endregion


        #region 私有方法

        /// <summary>
        /// 加入到缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        private void AddToCache<T>(T view) where T : IUIView
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 实例化一个可视对象
        /// </summary>
        /// <param name="goLocation"></param>
        /// <returns></returns>
        private TGameObject Instantiate(string goLocation, TGameObject parent)
        {
            return instantiator.Instantiate(goLocation, parent);
        }

        /// <summary>
        /// 异步实例化
        /// </summary>
        /// <param name="goLocation"></param>
        /// <returns></returns>
        private Task<TGameObject> InstantiateAsync(string goLocation, TGameObject parent)
        {
            return instantiator.InstantiateAsync(goLocation, parent);
        }

        /// <summary>
        /// 绑定游戏对象和脚本的接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        private T BindView<T>(TGameObject go) where T : IUIView
        {
            return viewBinder.BindView<T>(go);
        }

        public void Refresh<T>(T view) where T : IUIView
        {
            throw new NotImplementedException();
        }

        public void RefreshAll()
        {
            throw new NotImplementedException();
        }

        public void CloseAll()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
