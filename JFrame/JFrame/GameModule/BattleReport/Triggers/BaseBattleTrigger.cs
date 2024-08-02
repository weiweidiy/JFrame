using System;

namespace JFrame
{
    public abstract class BaseBattleTrigger : IBattleTrigger
    {
        //public event Action onTrigger;

        /// <summary>
        /// 拥有者
        /// </summary>
        public IBattleAction Owner { get; private set; }

        /// <summary>
        /// 触发器类型
        /// </summary>
        public virtual BattleTriggerType TriggerType { get => BattleTriggerType.Normal; }

        /// <summary>
        /// 战斗管理器
        /// </summary>
        protected IPVPBattleManager battleManager;

        /// <summary>
        /// 参数
        /// </summary>
        protected float[] args;

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
        /// 触发状态
        /// </summary>
        protected bool isOn;

        /// <summary>
        /// 是否可用
        /// </summary>
        bool isEnable = true;

        public BaseBattleTrigger(IPVPBattleManager battleManager, float[] args, float delay = 0)
        {
            this.battleManager = battleManager;
            this.args = args;
            this.delay = delay;
            this.delayed = delay == 0f; //如果延迟为0，视为已经延迟过了
            this.isOn = false;
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

            OnDelayCompleteEveryFrame();
        }

        protected virtual void OnDelayCompleteEveryFrame() { }
    

        /// <summary>
        /// 影响是否触发，如果设置为false , 则不会触发
        /// </summary>
        /// <param name="isOn"></param>
        public void SetEnable(bool isOn) => this.isEnable = isOn;
        public virtual bool GetEnable() => isEnable;

        //public void NotifyOnTrigger()
        //{
        //    if (GetEnable())
        //    {
        //        isValid = true;
        //        //onTrigger?.Invoke();
        //    }
                
        //}

        public virtual void OnAttach(IBattleAction action)
        {
            Owner = action;
        }

        /// <summary>
        /// 重新启动
        /// </summary>
        public virtual void Restart()
        {
            SetEnable(true);
            delta = 0f;
            isOn = false;
        }

        /// <summary>
        /// 是否生效
        /// </summary>
        /// <returns></returns>
        public bool IsOn()
        {
            return isOn;
        }

        /// <summary>
        /// 设置无效
        /// </summary>
        public void SetInValid()
        {
            isOn = false;
        }

        /// <summary>
        /// 获取CD
        /// </summary>
        /// <returns></returns>
        public float[] GetArgs()
        {
            return args;
        }

        /// <summary>
        /// 设置cd
        /// </summary>
        /// <param name="cd"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetArgs(float[] args)
        {
            this.args = args;
        }
    }
}