namespace JFramework.Game
{
    /// <summary>
    /// 受伤之前触发
    /// </summary>
    public class JCombatBeforeHurtTrigger : JCombatTriggerBase
    {
        IJCombatTargetable targetable;

        DamageTriggerArgs args = new DamageTriggerArgs();

        public JCombatBeforeHurtTrigger(float[] args) : base(args)
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
            args.DamageData = data;
            TriggerOn(args);
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
