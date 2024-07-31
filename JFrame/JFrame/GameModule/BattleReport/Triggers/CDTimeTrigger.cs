using System;

namespace JFrame
{
    /// <summary>
    /// 时间触发器：时间到了触发1次 arg = CD周期 type = 1
    /// </summary>
    public class CDTimeTrigger : BaseBattleTrigger
    {

        public CDTimeTrigger(IPVPBattleManager pVPBattleManager, float[] args, float delay = 0f) : base( pVPBattleManager, args, delay) { }

        public override float GetCD()
        {
            return GetDuration();
        }

        /// <summary>
        /// 获取周期
        /// </summary>
        /// <returns></returns>
        public float GetDuration()
        {
            return this.args[0];
        }

        /// <summary>
        /// 延迟完成
        /// </summary>
        protected override void OnDelayCompleteEveryFrame()
        {
            base.OnDelayCompleteEveryFrame();

            //更新cd
            if (delta >= GetDuration() && GetEnable())
            {
                isOn = true;
            }
            else
                isOn = false;
        }
    }
}