using System;

namespace JFrame
{
    /// <summary>
    /// 周期触发：arg = CD周期 type = 1
    /// </summary>
    public class CDTrigger : BaseBattleTrigger
    {

        public CDTrigger(IPVPBattleManager pVPBattleManager, float duration, float delay = 0f) : base( pVPBattleManager, duration, delay) { }

        /// <summary>
        /// 获取周期
        /// </summary>
        /// <returns></returns>
        float GetDuration()
        {
            return this.arg;
        }

        /// <summary>
        /// 延迟完成
        /// </summary>
        protected override void OnDelayComplete()
        {
            base.OnDelayComplete();

            //更新cd
            if (delta >= GetDuration() && GetEnable())
            {
                //通知外部已触发
                NotifyOnTrigger();

                delta = 0f;
            }
        }
    }
}