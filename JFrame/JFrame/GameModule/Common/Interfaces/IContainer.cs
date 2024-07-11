using System.Collections.Generic;



namespace JFrame
{
    /// <summary>
    /// 管理容器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IContainer<T> where T : IUnique
    {
        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="teamMember"></param>
        void Add(T member);
        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="uid"></param>
        void Remove(string uid);

        /// <summary>
        /// 更新成员
        /// </summary>
        /// <param name="teamMember"></param>
        void Update(T member);

        /// <summary>
        /// 获取指定id成员
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        T Get(string uid);

        /// <summary>
        /// 获取所有成员
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();
    }
}

