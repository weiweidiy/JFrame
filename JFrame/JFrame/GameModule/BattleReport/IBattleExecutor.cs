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
        void Execute(IBattleUnit caster, IBattleAction action, IBattleUnit unit, BattleReporter reporter/*, string reportUID*/);
        void Update(BattleFrame frame);
    }
}