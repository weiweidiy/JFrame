using System;
using System.Collections.Generic;

namespace JFrame
{
    public class CombatActionManager : BaseContainer<CombatAction>, ICombatUpdatable
    {
        public event Action<CombatExtraData> onTriggerOn; //满足触发条件
        public event Action<CombatExtraData> onStartExecuting; //开始释放
        public event Action<CombatExtraData> onStartCD;
        public event Action<CombatExtraData> onHittingTargets; //动作命中之前（单次所有目标）
        public event Action<CombatExtraData> onTargetsHittedComplete; //动作命中之后（所有目标）
        public event Action<CombatExtraData> onHittingTarget; //动作命中之前（单目标）
        public event Action<CombatExtraData> onTargetHittedComplete; //动作命中之后（单目标）

        bool isBusy;
        float curDuration = 0f;
        float deltaTime = 0f;

        public void Initialize(IExtraDataClaimable extraClaimable)
        {
            foreach(var action in GetAll())
            {
                action.onTriggerOn += Action_onTriggerOn;
                action.onStartExecuting += Action_onStartExecuting;
                action.onStartCD += Action_onStartCD;
                action.onHittingTargets += Action_onHittingTargets;
                action.onTargetsHittedComplete += Action_onTargetsHittedComplete;
                action.onHittingTarget += Action_onHittingTarget;
                action.onTargetHittedComplete += Action_onTargetHittedComplete;

                //设置透传参数
                action.ExtraData = extraClaimable.ExtraData;

                //切换到不可用状态
                action.SwitchToDisable();
                action.SwitchToTrigging();
            }
        }

        private void Action_onTargetHittedComplete(CombatExtraData extraData)
        {
            onTargetHittedComplete?.Invoke(extraData);
        }

        private void Action_onHittingTarget(CombatExtraData extraData)
        {
            onHittingTarget?.Invoke(extraData);
        }

        private void Action_onTargetsHittedComplete(CombatExtraData extraData)
        {
            onTargetsHittedComplete?.Invoke(extraData);
        }

        private void Action_onHittingTargets(CombatExtraData extraData)
        {
            onHittingTargets?.Invoke(extraData);
        }

        private void Action_onStartCD(CombatExtraData extraData)
        {
            onStartCD?.Invoke(extraData);
        }


        /// <summary>
        /// 满足触发条件了
        /// </summary>
        /// <param name="extraData"></param>
        private void Action_onTriggerOn(CombatExtraData extraData)
        {
            if (extraData.Action.Mode == ActionMode.Active)
                onTriggerOn?.Invoke(extraData);

            if (!isBusy && extraData.Action.Mode == ActionMode.Active)
            {
                curDuration = extraData.Action.SwitchToExecuting();
                isBusy = true;
                return;
            }

            if(extraData.Action.Mode == ActionMode.Passive)
                extraData.Action.SwitchToExecuting();
        }

        /// <summary>
        /// 开始释放了
        /// </summary>
        /// <param name="extraData"></param>
        private void Action_onStartExecuting(CombatExtraData extraData)
        {
            onStartExecuting?.Invoke(extraData);
        }


        public void Update(BattleFrame frame)
        {
            foreach (var action in GetAll())
            {
                action.Update(frame);
            }

            UpdateDuration(frame);
        }

        /// <summary>
        /// 更新释放时间
        /// </summary>
        /// <param name="frame"></param>
        public void UpdateDuration(BattleFrame frame)
        {
            if (isBusy)
            {
                deltaTime += frame.DeltaTime;

                if (deltaTime >= curDuration)
                {
                    isBusy = false;
                    deltaTime = 0f;
                }
            }
        }

        public void SetAllActive(bool active)
        {
            if (active)
            {
                foreach (var action in GetAll())
                {
                    action.SwitchToCd();
                }
            }
            else
            {
                foreach (var action in GetAll())
                {
                    action.SwitchToDisable();
                }
            }
        }

        //public void Start()
        //{
        //    foreach (var action in GetAll())
        //    {
        //        action.SwitchToTrigging();
        //    }
        //}
    }

}