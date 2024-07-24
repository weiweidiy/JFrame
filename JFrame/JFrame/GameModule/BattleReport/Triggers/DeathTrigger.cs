namespace JFrame
{
    /// <summary>
    /// 死亡触发，只能1次  arg : 没用  type = 2
    /// </summary>
    public class DeathTrigger : BaseBattleTrigger
    {
        bool valid = true;
        public DeathTrigger( IPVPBattleManager pvpBattleManager, float arg, float delay = 0):base(pvpBattleManager, arg, delay)
        { }

        public override void OnAttach(IBattleAction action)
        {
            base.OnAttach(action);

            action.Owner.onDead += Owner_onDead;
        }

        /// <summary>
        /// 持有者死亡了
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        private void Owner_onDead(IBattleUnit arg1, IBattleAction arg2, IBattleUnit arg3)
        {
            if(valid)
                NotifyOnTrigger();

            valid = false;
        }
    }
}