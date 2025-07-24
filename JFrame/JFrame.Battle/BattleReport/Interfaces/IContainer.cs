//using System;
//using System.Collections.Generic;
//using System.ComponentModel;



//namespace JFrame
//{
//    /// <summary>
//    /// 管理容器接口
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public interface IContainer<T> where T : IUnique
//    {
//        event Action<T> onItemAdded;

//        event Action<T> onItemRemoved;

//        event Action<T> onItemUpdated;

//        /// <summary>
//        /// 添加成员
//        /// </summary>
//        /// <param name="teamMember"></param>
//        void Add(T member);
//        /// <summary>
//        /// 删除成员
//        /// </summary>
//        /// <param name="Uid"></param>
//        bool Remove(string Uid);

//        /// <summary>
//        /// 更新成员
//        /// </summary>
//        /// <param name="teamMember"></param>
//        void Update(T member);

//        /// <summary>
//        /// 获取指定id成员
//        /// </summary>
//        /// <param name="Uid"></param>
//        /// <returns></returns>
//        T Get(string Uid);

//        /// <summary>
//        /// 获取所有成员
//        /// </summary>
//        /// <returns></returns>
//        List<T> GetAll();
//    }
//}

