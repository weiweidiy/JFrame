using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JFrame
{

    public class CombatAction : ICombatAction, ICombatUpdatable, IUnique, IActionOwner
    {
        public event Action<CombatExtraData> onTriggerOn;
        public event Action<CombatExtraData> onStartExecuting;
        public event Action<CombatExtraData> onStartCD;
        public event Action<CombatExtraData> onHittingTargets; //动作命中之前（单次所有目标）
        public event Action<CombatExtraData> onTargetsHittedComplete; //动作命中之后（单个目标）
        public event Action<CombatExtraData> onHittingTarget;
        public event Action<CombatExtraData> onTargetHittedComplete;

        public void NotifyTriggerOn()
        {
            onTriggerOn?.Invoke(ExtraData);
        }

        public void NotifyStartExecuting()
        {
            onStartExecuting?.Invoke(ExtraData);
        }

        public void NotifyStartCD()
        {
            onStartCD?.Invoke(ExtraData);
        }

        public void NotifyHittingTargets(CombatExtraData extraData)
        {
            //ExtraData = extraData;
            onHittingTargets?.Invoke(extraData);
        }

        public void NotifyHittedTargets(CombatExtraData extraData)
        {
            //ExtraData = extraData;
            onTargetsHittedComplete?.Invoke(extraData);
        }

        public void NotifyHittingTarget(CombatExtraData extraData)
        {
            onHittingTarget?.Invoke(extraData);
        }

        public void NotifyHittedTarget(CombatExtraData extraData)
        {
            onTargetHittedComplete?.Invoke(extraData);
        }

        public string Uid { get; private set; }

        public int Id { get; private set; }

        public ActionType Type { get; protected set; }

        public ActionMode Mode { get; private set; }

        public int GroupId { get; private set; }

        /// <summary>
        /// 透傳對象
        /// </summary>
        CombatExtraData _extraData;
        public CombatExtraData ExtraData
        {
            get => _extraData; 
            set
            {
                _extraData = value;
                _extraData.Action = this;

                foreach (var trigger in conditionTriggers)
                {
                    trigger.ExtraData = _extraData.Clone() as CombatExtraData;
                }
            }
        }

        /// <summary>
        /// 觸發器列表
        /// </summary>
        List<CombatBaseTrigger> conditionTriggers;

        /// <summary>
        /// 延遲出發器（從觸發到執行中間的延遲，有時間延遲類，還有距離/速度類等）
        /// </summary>
        CombatBaseTrigger delayTrigger;
        List<CombatBaseExecutor> executors;
        List<CombatBaseTrigger> cdTriggers;
        CombatActionSM sm;

        /// <summary>
        /// 初始化actions
        /// </summary>
        /// <param name="conditionTriggers"></param>
        /// <param name="finder"></param>
        /// <param name="executors"></param>
        /// <param name="cdTriggers"></param>
        public void Initialize(int id, string uid, ActionType type, ActionMode mode, int groupId, List<CombatBaseTrigger> conditionTriggers, CombatBaseTrigger delayTrigger, List<CombatBaseExecutor> executors, List<CombatBaseTrigger> cdTriggers, CombatActionSM sm)
        {
            Uid = uid;
            this.conditionTriggers = conditionTriggers;
            this.delayTrigger = delayTrigger;
            this.executors = executors;
            this.cdTriggers = cdTriggers;
            this.sm = sm;
            Id = id;

            this.Type = type;
            this.Mode = mode;

            //监听执行器命中等消息
            if(executors != null)
            {
                foreach (var executor in executors)
                {
                    executor.onHittingTargets += Executor_onHittingTargets; ;
                    executor.onTargetsHittedComplete += Executor_onTargetsHittedComplete;
                    executor.onHittingTarget += Executor_onHittingTarget;
                    executor.onTargetHittedComplete += Executor_onTargetHittedComplete;
                }
            }
        }

        private void Executor_onTargetHittedComplete(CombatExtraData extraData)
        {
            NotifyHittedTarget(extraData);
        }

        private void Executor_onHittingTarget(CombatExtraData extraData)
        {
            NotifyHittingTarget(extraData);
        }

        private void Executor_onHittingTargets(CombatExtraData extraData)
        {
            NotifyHittingTargets(extraData);
        }

        private void Executor_onTargetsHittedComplete(CombatExtraData extraData)
        {
            NotifyHittedTargets(extraData);
        }

        public void Start()
        {
            foreach (var trigger in conditionTriggers)
            {
                trigger.OnStart();
            }

            delayTrigger.OnStart();

            foreach (var executor in executors)
            {
                executor.OnStart();
            }

            foreach(var cdTrigger in cdTriggers)
            {
                cdTrigger.OnStart();
            }
        }

        public void Stop()
        {
            foreach (var trigger in conditionTriggers)
            {
                trigger.OnStop();
            }

            delayTrigger.OnStop();

            foreach (var executor in executors)
            {
                executor.OnStop();
            }

            foreach (var cdTrigger in cdTriggers)
            {
                cdTrigger.OnStop();
            }
        }

        public void Update(CombatFrame frame)
        {
            sm.Update(frame);
        }

        #region 给状态机调用的接口
        /// <summary>
        /// 更新條件觸發器
        /// </summary>
        /// <param name="frame"></param>
        public void UpdateConditionTriggers(CombatFrame frame)
        {
            foreach (var trigger in conditionTriggers)
            {
                trigger.Update(frame);
            }
        }

        /// <summary>
        /// 是否已經觸發（只要有1个触发器触发了就触发）
        /// </summary>
        /// <returns></returns>
        public bool IsConditionTriggerOn()
        {
            foreach (var trigger in conditionTriggers)
            {
                if (trigger.IsOn())
                {
                    ExtraData = trigger.ExtraData;
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

            foreach (var trigger in conditionTriggers)
            {
                trigger.Reset();
            }
        }

        public void EnterConditionTriggers()
        {
            if (conditionTriggers == null)
                return;
            foreach (var trigger in conditionTriggers)
            {
                trigger.OnEnterState();
            }
        }

        public void ExitConditionTriggers()
        {
            if (conditionTriggers == null)
                return;
            foreach (var trigger in conditionTriggers)
            {
                trigger.OnExitState();
            }
        }


        public void UpdateDelayTrigger(CombatFrame frame)
        {
            delayTrigger.Update(frame);
        }

        public bool IsDelayTriggerOn()
        {
            return delayTrigger.IsOn();
        }

        public void ResetDelayTrigger()
        {
            delayTrigger?.Reset();
        }

        public void DoExecutors()
        {
            if (executors == null)
                return;
            foreach (var executor in executors)
            {
                executor.Execute(ExtraData);
            }
        }

        public void UpdateExecutors(CombatFrame frame)
        {
            if (executors == null)
                return;
            foreach (var executor in executors)
            {
                executor.Update(frame);
            }
        }

        public void ResetExecutors()
        {
            if (executors == null)
                return;

            foreach (var executor in executors)
            {
                executor.Reset();
            }
        }

        public void UpdateCdTriggers(CombatFrame frame)
        {
            foreach (var trigger in cdTriggers)
            {
                trigger.Update(frame);
            }
        }

        public void EnterCdTriggers()
        {
            if (cdTriggers == null)
                return;
            foreach (var trigger in cdTriggers)
            {
                trigger.OnEnterState();
            }
        }

        public void ExitCdTriggers()
        {
            if (cdTriggers == null)
                return;

            foreach (var trigger in cdTriggers)
            {
                trigger.OnExitState();
            }
        }

        /// <summary>
        /// 是否已經觸發 (必须所有触发器都触发才算触发）
        /// </summary>
        /// <returns></returns>
        public bool IsCdTriggerOn()
        {
            bool isCdTriggerOn = true;
            foreach (var trigger in cdTriggers)
            {
                if (!trigger.IsOn())
                {
                    isCdTriggerOn = false;
                }
            }
            return isCdTriggerOn;
        }

        public void ResetCdTriggers()
        {
            if (cdTriggers == null)
                return;

            foreach (var trigger in cdTriggers)
            {
                trigger.Reset();
            }
        }
        #endregion

        public void SwitchToDisable()
        {
            var curState = sm.GetCurState();
            if (curState.Name == nameof(ActionDisableState))
                return;

            sm.SwitchToDisable();
        }

        public void SwitchToTrigging()
        {
            sm.SwitchToStandby();
        }

        public float SwitchToExecuting()
        {
            var duration = GetExecutingDuration();
            ExtraData.CastDuration = duration;

            sm.SwitchToExecuting();

            return duration;
        }

        public float SwitchToCd()
        {
            var duration = GetCdTriggerDuration();
            ExtraData.CdDuration = duration;
            sm.SwitchToCding();
            return duration;
        }

        /// <summary>
        /// 獲取執行時常
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public float GetExecutingDuration()
        {
            if (executors == null)
                return 0;

            float duration = 0f;
            foreach (var executor in executors)
            {
                var d = executor.GetDuration();
                if (d > duration)
                    duration = d;
            }
            return duration;
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


    }




}