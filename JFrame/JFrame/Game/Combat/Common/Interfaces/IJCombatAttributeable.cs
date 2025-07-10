using JFramework;

namespace JFramework.Game
{
    /// <summary>
    /// 可属性化接口
    /// </summary>
    public interface IJCombatAttributeable : IUnique
    {
        /// <summary>
        /// 是否已死亡
        /// </summary>
        /// <returns></returns>
        bool IsDead();

        /// <summary>
        /// 获取属性对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uid"></param>
        /// <returns></returns>
        IUnique GetAttribute(string uid);
    }
}
