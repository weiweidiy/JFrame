using JFramework;
using System;
using System.Collections.Generic;

namespace JFrame.Game
{
    /// <summary>
    /// 战斗行动的选择器 
    /// </summary>
    public interface ITurnBasedCombatActionSelector
    {
        /// <summary>
        /// 获取出手序列（已排序）
        /// </summary>
        /// <returns></returns>
        List<IJTurnBasedCombatUnit> GetActionUnits();
        /// <summary>
        /// 获取当前可行动的单位
        /// </summary>
        /// <returns></returns>
        IJTurnBasedCombatUnit PopActionUnit();

        /// <summary>
        /// 重置actionList
        /// </summary>
        void ResetActionUnits();

        /// <summary>
        /// 设置列表（未排序）
        /// </summary>
        /// <param name="units"></param>
        void SetUnits(List<IJTurnBasedCombatUnit> units);

        /// <summary>
        /// 是否全部完成了
        /// </summary>
        /// <returns></returns>
        bool IsAllComplete();
    }


}
