namespace JFrame
{
    /// <summary>
    /// 战斗开始触发，只触发1次  arg: 没用 type = 3
    /// </summary>
    public class BattleStartTrigger : BaseBattleTrigger
    {
        bool valid = true;
        public BattleStartTrigger(IPVPBattleManager battleManager, float arg, float delay = 0) : base(battleManager, arg, delay)
        {
        }

        protected override void OnDelayComplete()
        {
            base.OnDelayComplete();

            if(valid)
            {
                NotifyOnTrigger();
                valid = false;
            }
        }
    }
}