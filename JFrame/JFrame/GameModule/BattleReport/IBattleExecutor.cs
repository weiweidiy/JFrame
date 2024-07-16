using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 战斗效果执行器，实际的技能效果，比如伤害，加血，加BUFF等
    /// </summary>
    public interface IBattleExecutor
    {
        void Execute(IBattleUnit caster, IBattleUnit unit, BattleReporter reporter/*, string reportUID*/);
        void Update(BattleFrame frame);
    }
}