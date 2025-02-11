namespace JFrame
{
    /// <summary>
    /// 开始战斗时候触发 type = 5
    /// </summary>
    public class TriggerCombatStart : CombatBaseTrigger
    {
        public TriggerCombatStart(CombatBaseFinder finder) : base(finder)
        {
        }

        public override int GetValidArgsCount()
        {
            return 0;
        }

        public override void OnStart()
        {
            base.OnStart();

            SetOn(true);
        }
    }
}