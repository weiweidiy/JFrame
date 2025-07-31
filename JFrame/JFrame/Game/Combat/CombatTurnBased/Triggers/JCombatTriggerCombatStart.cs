using System;

namespace JFramework.Game
{
    /// <summary>
    /// 战斗开始时，触发一次
    /// </summary>
    public class JCombatTriggerCombatStart : JCombatTriggerBase
    {
        public JCombatTriggerCombatStart(float[] args, IJCombatTargetsFinder finder) : base(args, finder)
        {
        }

        protected override int GetValidArgsCount()
        {
            return 0;
        }

        protected override void OnStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);

            TriggerOn(null); // 战斗开始时触发一次
        }
    }


}
