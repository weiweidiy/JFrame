using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 战斗单元接口
    /// </summary>
    public interface IBattleUnit
    {
        event Action<IBattleUnit, IBattleAction, List<IBattleUnit>> onActionReady;

        /// <summary>
        /// 是否活着
        /// </summary>
        /// <returns></returns>
        bool IsAlive();
    }
}

