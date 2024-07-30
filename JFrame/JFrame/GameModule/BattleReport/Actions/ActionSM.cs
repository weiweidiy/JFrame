using Stateless;
using System;
using System.Xml;


namespace JFrame
{

    public class ActionSM
    {
        enum Trigger
        {
            Disable = 0,
            Standby,
            Execute,
            CD
        }

        StateMachine<ActionState, Trigger> machine;

        BaseAction context;

        public void Initialize(BaseAction context)
        {
            this.context = context;

            var disableState = new ActionDisable();
            var standbyState = new ActionStandby();
            var executingState = new ActionExecuting();
            var cdingState = new ActionCding();
            disableState.Fsm = this;
            standbyState.Fsm = this;
            executingState.Fsm = this;
            cdingState.Fsm = this;

            //配置游戏状态机
            machine = new StateMachine<ActionState, Trigger>(disableState /*() => _state, s => _state = s*/);

            machine.Configure(disableState)
                .OnEntry(() => { OnEnterDisable(disableState); })
                .OnExit(() => { OnExitDisable(disableState); })
                .Permit(Trigger.Standby, standbyState)
                .Permit(Trigger.CD, cdingState);
                //.Ignore(Trigger.Disable);

            //待释放
            machine.Configure(standbyState)
                .OnEntry(() => { OnEnterStandby(standbyState); })
                .OnExit(() => { OnExitStandby(standbyState); })
                .Permit(Trigger.Disable, disableState)
                .Permit(Trigger.Execute, executingState);

            //释放中
            machine.Configure(executingState)
                .OnEntry(() => { OnEnterExecuting(executingState); })
                .OnExit(() => { OnExitExecuting(executingState); })
                .Permit(Trigger.Disable, disableState)
                .Permit(Trigger.CD, cdingState);

            //cd中
            machine.Configure(cdingState)
                .OnEntry(() => { OnEnterCding(cdingState); })
                .OnExit(() => { OnExitCding(cdingState); })
                .Permit(Trigger.Disable, disableState)
                .Permit(Trigger.Standby, standbyState);

        }

        /// <summary>
        /// 切换到游戏状态
        /// </summary>
        public void SwitchToDisable()
        {
            machine.Fire(Trigger.Disable);
        }


        public void SwitchToStandby()
        {
            //Debug.LogError("SwitchToReady");
             machine.Fire(Trigger.Standby);
        }

        public void SwitchToExecuting()
        {
            //Debug.LogError("SwitchToRunning");
            machine.Fire(Trigger.Execute);
        }

        public void SwitchToCding()
        {
            //Debug.LogError("SwitchToEnding");
            machine.Fire(Trigger.CD);
        }



        /// <summary>
        /// 进入事件
        /// </summary>
        /// <param name="readyState"></param>
        /// <returns></returns>
        private void OnEnterDisable(ActionDisable disableState)
        {
            disableState.OnEnter(context);
        }


        private void OnEnterStandby(ActionStandby readyState)
        {
             readyState.OnEnter(context);
        }

        private  void OnEnterExecuting(ActionExecuting runningState)
        {
             runningState.OnEnter(context);
        }

        private  void OnEnterCding(ActionCding endingState)
        {
             endingState.OnEnter(context);
        }

        /// <summary>
        /// 退出事件
        /// </summary>
        /// <param name="disableState"></param>
        private void OnExitDisable(ActionDisable disableState)
        {
            disableState.OnExit();
        }


        private void OnExitStandby(ActionStandby readyState)
        {
             readyState.OnExit();
        }

        private  void OnExitExecuting(ActionExecuting runningState)
        {
             runningState.OnExit();
        }

        private  void OnExitCding(ActionCding endingState)
        {
             endingState.OnExit();
        }

        public void Update(BattleFrame frame)
        {
            machine.State.Update(frame);
        }

        /// <summary>
        /// 查询接口
        /// </summary>
        /// <returns></returns>
        public string GetCurState()
        {
            return machine.State.Name;
        }
    }
}