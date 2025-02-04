using System;
using System.Collections.Generic;

namespace JFrame
{
    public class CombatAction : ICombatAction, ICombatUpdatable, IUnique
    {
        public event Action<CombatExtraData> onTriggerOn;
        public event Action<ICombatAction, List<ICombatUnit>, float> onStartCast;
        public event Action<ICombatAction, float> onStartCD;
        public event Action<ICombatAction, ICombatUnit, ExecuteInfo> onHittingTarget;
        public event Action<ICombatAction, ICombatUnit, ExecuteInfo, ICombatUnit> onHittedComplete;

        public void NotifyTriggerOn()
        {
            onTriggerOn?.Invoke(ExtraData);
        }

        protected void NotifyStartCast(List<ICombatUnit> targets, float duration)
        {
            throw new NotImplementedException();
        }

        protected void NotifyStartCD(float cd)
        {
            throw new NotImplementedException();
        }

        public string Uid { get; private set; }

        public ActionType Type { get; protected set; }

        public ActionMode Mode { get; private set; }

        /// <summary>
        /// 透傳對象
        /// </summary>
        public CombatExtraData ExtraData { get; private set; }

        public int Id { get; private set; }

        /// <summary>
        /// 觸發器列表
        /// </summary>
        List<BaseTrigger> conditionTriggers;

        /// <summary>
        /// 延遲出發器（從觸發到執行中間的延遲，有時間延遲類，還有距離/速度類等）
        /// </summary>
        BaseTrigger delayTrigger;
        List<BaseExecutor> executors;
        List<ICombatTrigger> cdTriggers;
        ActionSM sm;

        /// <summary>
        /// 初始化actions
        /// </summary>
        /// <param name="conditionTriggers"></param>
        /// <param name="finder"></param>
        /// <param name="executors"></param>
        /// <param name="cdTriggers"></param>
        public void Initialize(int id, ActionType type, ActionMode mode, List<BaseTrigger> conditionTriggers, BaseTrigger delayTrigger, List<BaseExecutor> executors, List<ICombatTrigger> cdTriggers, ActionSM sm)
        {
            Uid = Guid.NewGuid().ToString();
            this.conditionTriggers = conditionTriggers;
            this.delayTrigger = delayTrigger;
            this.executors = executors;
            this.cdTriggers = cdTriggers;
            this.sm = sm;
            Id = id;
            this.Type = type;
            this.Mode = mode;
        }

        public void Update(BattleFrame frame)
        {
            sm.Update(frame);
        }

        /// <summary>
        /// 更新條件觸發器
        /// </summary>
        /// <param name="frame"></param>
        public void UpdateConditionTriggers(BattleFrame frame)
        {
            foreach (var trigger in conditionTriggers)
            {
                trigger.Update(frame);
            }
        }

        /// <summary>
        /// 是否已經觸發
        /// </summary>
        /// <returns></returns>
        public bool IsConditionTriggerOn()
        {
            foreach (var trigger in conditionTriggers)
            {
                if(trigger.IsOn())
                {
                    ExtraData = trigger.CombatExtraData;
                    return true;
                }                
            }
            return false;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void ResetConiditionTriggers()
        {
            if (conditionTriggers == null)
                return;

            foreach(var trigger in conditionTriggers)
            {
                trigger.Reset();
            }
        }


        public void UpdateDelayTrigger(BattleFrame frame)
        {
            delayTrigger.Update(frame);
        }

        public bool IsDelayTriggerOn()
        {
            return delayTrigger.IsOn();
        }

        public void ResetDelayTrigger()
        {
            delayTrigger.Reset();
        }


        public void UpdateExecutors(BattleFrame frame)
        {
            foreach(var executor in executors)
            {
                executor.Update(frame);
            }
        }


        public void SwitchToDisable()
        {
            sm.SwitchToDisable();
        }

        public void SwitchToTrigging()
        {
            sm.SwitchToStandby();
        }

        public float SwitchToExecuting()
        {
            sm.SwitchToExecuting();
            return GetExecutingDuration();
        }

        public float SwitchToCd()
        {
            sm.SwitchToCding();
            return GetCdTriggerDuration();
        }

        /// <summary>
        /// 獲取執行時常
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private float GetExecutingDuration()
        {
            return 0;
            //foreach(var executor in executors)
            //{
            //    if(executor is executor)
            //}
        }

        /// <summary>
        /// 獲取cd時長
        /// </summary>
        /// <returns></returns>
        public float GetCdTriggerDuration()
        {
            foreach (var trigger in cdTriggers)
            {

                if (trigger is TriggerTime)
                {
                    var triggerTime = trigger as TriggerTime;
                    return triggerTime.GetDuration();
                }
            }
            return 0f;
        }

        //public bool CanCast()
        //{
        //    throw new NotImplementedException();
        //}

        //public float SwitchToExecuting()
        //{
        //    throw new NotImplementedException();
        //}

        //public float SwitchToCd()
        //{
        //    throw new NotImplementedException();
        //}

        //public List<ICombatUnit> FindTargets(object[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public float GetCastDuration()
        //{
        //    throw new NotImplementedException();
        //}

        //public float[] GetCdArgs()
        //{
        //    throw new NotImplementedException();
        //}

        //public IBattleTrigger GetCDTrigger()
        //{
        //    throw new NotImplementedException();
        //}

        //public float[] GetConditionTriggerArgs()
        //{
        //    throw new NotImplementedException();
        //}

        //public string GetCurState()
        //{
        //    throw new NotImplementedException();
        //}

        //public float[] GetExecutorArgs()
        //{
        //    throw new NotImplementedException();
        //}

        //public float[] GetFinderArgs()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Interrupt()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool IsCDComplete()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool IsExecuting()
        //{
        //    throw new NotImplementedException();
        //}

        //public void NotifyCanCast()
        //{
        //    throw new NotImplementedException();
        //}

        //public void NotifyStartCast(List<ICombatUnit> targets, float duration)
        //{
        //    throw new NotImplementedException();
        //}

        //public void NotifyStartCD(float cd)
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnEnable()
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnStart()
        //{
        //    throw new NotImplementedException();
        //}

        //public void OnStop()
        //{
        //    throw new NotImplementedException();
        //}

        //public void ReadyToExecute(ICombatUnit caster, ICombatAction action, List<ICombatUnit> targets)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ResetCdArgs(float[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ResetConditionTriggerArgs(float[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ResetExecutorArgs(float[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ResetFinderArgs(float[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetCdArgs(float[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetConditionTriggerArgs(float[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetEnable(bool enable)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetExecutorArgs(float[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetFinderArgs(float[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SwitchToDisable()
        //{
        //    throw new NotImplementedException();
        //}

        //public void SwitchToTrigging()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update(BattleFrame frame)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateConditionTriggers(BattleFrame frame)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateExecutors(BattleFrame frame)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateCdTriggers(BattleFrame frame)
        //{ throw new NotImplementedException(); }

        //public void SetCurArgs(float[] args)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetCurArg(int index, float arg)
        //{
        //    throw new NotImplementedException();
        //}

        //public float GetCurArg(int index)
        //{
        //    throw new NotImplementedException();
        //}

        //public float[] GetCurArgs()
        //{
        //    throw new NotImplementedException();
        //}

        //public float[] GetOriginArgs()
        //{
        //    throw new NotImplementedException();
        //}

        //public void ResetArgs()
        //{
        //    throw new NotImplementedException();
        //}
    }




}