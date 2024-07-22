using System;

namespace JFrame
{
    public class CDTrigger : BattleTrigger
    {
        /// <summary>
        /// 触发周期
        /// </summary>
        float duration;

        /// <summary>
        /// 首次延迟
        /// </summary>
        float delay;

        /// <summary>
        /// 是否已经延迟过了
        /// </summary>
        bool delayed;

        /// <summary>
        /// 临时变量
        /// </summary>
        float delta;

        public CDTrigger(float duration, float delay = 0f)
        {
            this.duration = duration;
            this.delay = delay;
            delayed = delay == 0f; //如果延迟为0，视为已经延迟过了
        }

        /// <summary>
        /// 更新帧
        /// </summary>
        /// <param name="frame"></param>
        public override void Update(BattleFrame frame)
        {
            if (!GetEnable())
                return;

            delta += frame.DeltaTime;

            if (!delayed)
            {
                if (delta - delay > 0f)
                {
                    delta -= delay;
                    delayed = true;
                }
                return;
            }
                
            //更新cd
            if (delta >= duration && GetEnable())
            {
                //通知外部已触发
                NotifyOnTrigger();

                delta = 0f;
            }
        }
    }
}