namespace JFrame
{
    /// <summary>
    /// cd中
    /// </summary>
    public class ActionCding : ActionState
    {
        public override string Name => nameof(ActionCding);



        public override void OnEnter(BaseAction context)
        {
            base.OnEnter(context); //要先调用

            //cd触发器开始运行
            if (NeedUpdate())
            {
                context.cdTrigger.Restart();
                context.NotifyStartCD(context.cdTrigger.GetArgs()[0]);
            }
                
        }


        public override void OnExit()
        {
            //Debug.Log("BatlleReady OnExit");
            base.OnExit();
        }

        public override void Update(BattleFrame frame)
        {
            base.Update(frame);

            if (NeedUpdate())
                context.cdTrigger.Update(frame);

            if (NeedUpdate() && context.IsCDComplete())
            {
                //设置触发器无效
                context.cdTrigger.SetInValid();

                //动作进入待机状态
                context.Standby();
            }
        }

        bool NeedUpdate()
        {
            return context.cdTrigger != null && (context.cdTrigger.TriggerType == BattleTriggerType.Normal || context.cdTrigger.TriggerType == BattleTriggerType.All);
        }
    }
}