using System;

namespace JFrame
{
    public interface ICombatExecutor
    {
        /// <summary>
        /// 即将命中目标
        /// </summary>
        event Action<CombatExtraData> onHittingTarget;

        /// <summary>
        /// 命中完成
        /// </summary>
        event Action<CombatExtraData> onHittedComplete;

        /// <summary>
        /// 执行
        /// </summary>
        void Execute(CombatExtraData extraData);
    }


}