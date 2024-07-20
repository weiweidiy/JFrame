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

        bool Active { get; set; }
        void Hit(IBattleUnit caster, IBattleAction action, IBattleUnit unit);

        void ReadyToExecute(IBattleUnit caster, IBattleAction action, IBattleUnit unit);

        void Update(BattleFrame frame);
    }
}