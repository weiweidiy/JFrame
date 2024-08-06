using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 普通的一次行动逻辑 (瞬时动作类型) 
    /// </summary>
    public class NormalAction : BaseAction
    {
        public NormalAction(string UID, int id, ActionType type, float duration, IBattleTrigger conditionTrigger, IBattleTargetFinder finder, List<IBattleExecutor> exutors, IBattleTrigger cdTrigger, ActionSM sm) : base(UID, id, type, duration, conditionTrigger, finder, exutors, cdTrigger, sm)
        {
        }
    }
}