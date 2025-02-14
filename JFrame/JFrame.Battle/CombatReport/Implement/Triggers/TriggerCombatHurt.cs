namespace JFrame
{
    /// <summary>
    /// type 4  有人即将受到伤害（还没有） 参数0：谁即将受伤害（0=友军，1=敌军，2=持有者）
    /// </summary>
    public class TriggerCombatHurt : CombatBaseTrigger
    {
        public TriggerCombatHurt(CombatBaseFinder finder) : base(finder)
        {
        }

        public override int GetValidArgsCount()
        {
            return 1;
        }

        public int GetTargetArg()
        {
            return (int)GetCurArg(0);
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
        }

        public override void OnExitState()
        {
            base.OnExitState();
        }
    }
}