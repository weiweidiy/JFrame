using System;

namespace JFrame
{
    /// <summary>
    /// 执行中状态
    /// </summary>
    public class ActionExecutingState : BaseActionState
    {
        //public event Action onExecutingComplete;
        float executingDuration;


        float deltaTime = 0f;

        protected override void OnEnter()
        {
            base.OnEnter();
            context.ResetExecutors(); 
            context.NotifyStartExecuting();
            executingDuration = context.GetExecutingDuration();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void Update(BattleFrame frame)
        {
            base.Update(frame);

            context.UpdateDelayTrigger(frame);
            if(context.IsDelayTriggerOn())
            {
                context.DoExecutors();
                context.UpdateExecutors(frame);

                deltaTime += frame.DeltaTime;

                if (deltaTime >= executingDuration)
                {
                    deltaTime = 0f;
                    var cdDuration = context.SwitchToCd();
                }
            }
        }


    }
}
