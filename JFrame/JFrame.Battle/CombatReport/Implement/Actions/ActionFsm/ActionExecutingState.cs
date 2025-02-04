using System;

namespace JFrame
{
    /// <summary>
    /// 执行中状态
    /// </summary>
    public class ActionExecutingState : BaseActionState
    {
        public event Action onExecutingComplete;

        protected override void OnEnter()
        {
            base.OnEnter();
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
                context.UpdateExecutors(frame);
            }
        }


    }
}
