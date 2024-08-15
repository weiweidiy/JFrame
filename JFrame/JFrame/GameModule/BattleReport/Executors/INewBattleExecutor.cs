using System;
using System.Collections.Generic;

namespace JFrame
{
    public interface INewBattleExecutor :  IAttachable
    {
        /// <summary>
        /// 即将命中目标
        /// </summary>
        event Action<IBattleUnit, ExecuteInfo> onHittingTarget;

        /// <summary>
        /// 是否激活
        /// </summary>
        bool Active { get; }

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

        /// <summary>
        /// 重新激活技能
        /// </summary>
        void Reset();
    }
}