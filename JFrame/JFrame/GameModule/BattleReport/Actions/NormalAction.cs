using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 普通的一次行动逻辑 (瞬时动作类型) 
    /// </summary>
    public class NormalAction : BaseAction
    {
        public NormalAction(int id, IBattleTrigger trigger, IBattleTargetFinder finder, List<IBattleExecutor> exutors) : base(id, trigger, finder, exutors)
        {
        }
    }
}