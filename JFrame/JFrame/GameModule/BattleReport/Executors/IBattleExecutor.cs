using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 战斗效果执行器，实际的技能效果，比如伤害，加血，加BUFF等
    /// </summary>
    public interface IBattleExecutor
    {
        event Action onExecute;

        /// <summary>
        /// 是否激活
        /// </summary>
        bool Active { get; }

        IBattleAction Owner { get; }

        /// <summary>
        ///  命中效果
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="target"></param>
        void Hit(IBattleUnit caster, IBattleAction action, List<IBattleUnit> target);

        /// <summary>
        /// 准备开始执行
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="targets"></param>
        void ReadyToExecute(IBattleUnit caster, IBattleAction action, List<IBattleUnit> targets);

        /// <summary>
        /// 更新帧，可以延迟命中
        /// </summary>
        /// <param name="frame"></param>
        void Update(BattleFrame frame);

        void OnAttach(IBattleAction action);
    }
}