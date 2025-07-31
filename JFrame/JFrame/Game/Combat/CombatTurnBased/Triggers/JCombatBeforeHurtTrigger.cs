using System.Collections.Generic;

namespace JFramework.Game
{
    /// <summary>
    /// 受伤之前触发
    /// </summary>
    public class JCombatBeforeHurtTrigger : JCombatTriggerBase
    {
        IJCombatTargetable targetable;

        public JCombatBeforeHurtTrigger(float[] args, IJCombatTargetsFinder finder) : base(args, finder)
        {
        }

        protected override int GetValidArgsCount()
        {
            return 0; // 不需要参数
        }

        protected override void OnStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);

            var casterUid = GetOwner().GetCaster();
            var caster = query.GetUnit(casterUid);
            targetable = caster as IJCombatTargetable;
            targetable.onBeforeHurt += OnBeforeHurt;
        }

        private void OnBeforeHurt(IJCombatTargetable targetable, IJCombatDamageData data)
        {
            executeArgs.DamageData = data;
            executeArgs.TargetUnits = new List<IJCombatCasterTargetableUnit> { targetable as IJCombatCasterTargetableUnit };
            TriggerOn(executeArgs);
        }

        protected override void OnStop()
        {
            base.OnStop();

            if (targetable != null)
            {
                targetable.onBeforeHurt -= OnBeforeHurt;
                targetable = null;
            }
        }


    }


}
