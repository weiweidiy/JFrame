using System;

namespace JFrame
{
    public abstract class BaseBattleTrigger : IBattleTrigger
    {
        public event Action onTrigger;

        /// <summary>
        /// 拥有者
        /// </summary>
        public IBattleAction Owner { get; private set; }

        protected IPVPBattleManager battleManager;

        /// <summary>
        /// 参数
        /// </summary>
        protected float arg;

        /// <summary>
        /// 延迟触发
        /// </summary>
        protected float delay;
        /// <summary>
        /// 是否已经延迟过了
        /// </summary>
        protected bool delayed;

        /// <summary>
        /// 临时变量，记录流逝时间
        /// </summary>
        protected float delta;

        /// <summary>
        /// 是否可用
        /// </summary>
        bool isOn = true;

        public BaseBattleTrigger(IPVPBattleManager battleManager, float arg, float delay = 0)
        {
            this.battleManager = battleManager;
            this.arg = arg;
            this.delay = delay;
            this.delayed = delay == 0f; //如果延迟为0，视为已经延迟过了
        }

 

        /// <summary>
        /// 更新帧
        /// </summary>
        /// <param name="frame"></param>
        public virtual void Update(BattleFrame frame)
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

            OnDelayComplete();
        }

        protected virtual void OnDelayComplete()
        {

        }
    

        /// <summary>
        /// 影响是否触发，如果设置为false , 则不会触发
        /// </summary>
        /// <param name="isOn"></param>
        public void SetEnable(bool isOn) => this.isOn = isOn;
        public bool GetEnable() => isOn;

        public void NotifyOnTrigger()
        {
            if (isOn)
                onTrigger?.Invoke();
        }

        public virtual void OnAttach(IBattleAction action)
        {
            Owner = action;
        }
    }
}