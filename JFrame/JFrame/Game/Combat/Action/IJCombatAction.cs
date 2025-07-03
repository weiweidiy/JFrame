using JFramework;
using System.Collections.Generic;

namespace JFrame.Game
{
    public interface IJCombatAction : IUnique
    {
        /// <summary>
        /// 执行一次动作（触发，攻击）
        /// </summary>
        /// <param name="query"></param>
        void Run(IJCombatQuery query);

        /// <summary>
        /// 直接执行action效果
        /// </summary>
        /// <param name="targets"></param>
        void Execute(List<IJCombatUnit> targets);
    }
}
