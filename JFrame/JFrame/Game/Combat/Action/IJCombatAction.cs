using JFramework;
using System.Collections.Generic;

namespace JFramework.Game
{
    public interface IJCombatAction : IUnique , IJCombatLifeCycle , IJCombatCastable
    {
        /// <summary>
        /// 设置归属
        /// </summary>
        /// <param name="casterQuery"></param>
        void SetCaster(IJcombatCasterQuery casterQuery);

        /// <summary>
        /// 获取释放者
        /// </summary>
        /// <returns></returns>
        string GetCaster();
    }

    //public interface IJCombatCast
}
