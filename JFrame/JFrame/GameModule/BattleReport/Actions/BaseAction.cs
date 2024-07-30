using System;
using System.Collections.Generic;

namespace JFrame
{

    /// <summary>
    /// 普通的一次行动逻辑 (瞬时动作类型)
    /// </summary>
    public abstract class BaseAction : IBattleAction
    {
 
        /// <summary>
        /// 可以触发了
        /// </summary>
        public event Action<IBattleAction> onCanCast;

        /// <summary>
        /// 开始释放了
        /// </summary>
        public event Action<IBattleAction, List<IBattleUnit>,float> onStartCast;


        /// <summary>
        /// 动作名称
        /// </summary>
        public string Name => nameof(BaseAction);

        /// <summary>
        /// 动作ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 拥有者
        /// </summary>
        public IBattleUnit Owner { get; private set; }

        /// <summary>
        /// 唯一ID
        /// </summary>
        public string Uid { get; private set; }

        /// <summary>
        /// 是否持续时间
        /// </summary>
        float castDuration;

        /// <summary>
        /// 条件触发器
        /// </summary>
        public IBattleTrigger ConditionTrigger { get; private set; }

        /// <summary>
        /// 冷却触发器
        /// </summary>
        public IBattleTrigger cdTrigger { get; private set; }

        /// <summary>
        /// 目标搜索器
        /// </summary>
        public IBattleTargetFinder finder { get; private set; }

        /// <summary>
        /// 效果执行器
        /// </summary>
        public List<IBattleExecutor> exeutors { get; private set; }

        ActionSM sm;

        /// <summary>
        /// 常规动作逻辑，触发器触发->搜索敌人->执行效果
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trigger"></param>
        /// <param name="finder"></param>
        /// <param name="exutor"></param>
        public BaseAction(string UID, int id, float duration, IBattleTrigger trigger, IBattleTargetFinder finder, List<IBattleExecutor> exutors, IBattleTrigger cdTrigger , ActionSM sm)
        {
            this.castDuration = duration;
            this.Uid = UID;
            this.Id = id;
            this.ConditionTrigger = trigger;
            this.finder = finder;
            this.exeutors = exutors;
            this.cdTrigger = cdTrigger;

            this.sm = sm;
            this.sm.Initialize(this);
        }

        /// <summary>
        /// 附加到单位上
        /// </summary>
        /// <param name="owner"></param>
        public void OnAttach(IBattleUnit owner)
        {
            Owner = owner;
            if (ConditionTrigger != null)
                ConditionTrigger.OnAttach(this);

            if (finder != null)
                finder.OnAttach(this);

            if (cdTrigger != null)
                cdTrigger.OnAttach(this);

            if (exeutors != null)
            {
                foreach (var executor in exeutors)
                {
                    executor.OnAttach(this);
                }
            }

            sm.SwitchToStandby();
        }

        #region 状态切换
        /// <summary>
        /// 进入待机状态
        /// </summary>
        public void Standby()
        {
            sm.SwitchToStandby();
        }


        /// <summary>
        /// 向指定目标释放
        /// </summary>
        /// <param name="units"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        public float Cast()
        {
            sm.SwitchToExecuting();
            return GetCastDuration();
        }

        /// <summary>
        /// 进入CD
        /// </summary>
        public void EnterCD()
        {
            sm.SwitchToCding();
        }
        #endregion

        
        /// <summary>
        /// 是否cd完成
        /// </summary>
        /// <returns></returns>
        public bool IsCDComplete()
        {
            return cdTrigger.IsOn();
        }

        /// <summary>
        /// 是否满足释放条件
        /// </summary>
        /// <returns></returns>
        public bool CanCast()
        {
            return ConditionTrigger.IsOn();
        }


        /// <summary>
        /// 更新帧
        /// </summary>
        /// <param name="frame"></param>
        public void Update(BattleFrame frame)
        {
            sm.Update(frame);
        }



        /// <summary>
        /// 设置动作是否可用
        /// </summary>
        /// <param name="dead"></param>
        public void SetDead(bool dead)
        {
            if (!dead)
            {
                sm.SwitchToCding(); //直接进入cd
            }
            else
            {
                sm.SwitchToDead();
            }
        }


        public void NotifyCanCast()
        {
            onCanCast?.Invoke(this);
        }

        public void NotifyStartCast(List<IBattleUnit> targets, float duration)
        {
            onStartCast(this, targets, duration);
        }


        //public void NotifyCdComplete()
        //{
        //    onCdComplete?.Invoke(this);
        //}

        /// <summary>
        /// 搜索目标
        /// </summary>
        /// <returns></returns>
        public List<IBattleUnit> FindTargets()
        {
            return finder.FindTargets();
        }

        /// <summary>
        /// 准备执行效果
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="targets"></param>
        public void ReadyToExecute(IBattleUnit caster, IBattleAction action, List<IBattleUnit> targets)
        {
            foreach (var e in exeutors)
            {
                e.ReadyToExecute(caster, this, targets);
            }
        }

        public float GetCastDuration()
        {
            return castDuration;
        }

        public string GetCurState()
        {
            return sm.GetCurState();
        }
    }
}