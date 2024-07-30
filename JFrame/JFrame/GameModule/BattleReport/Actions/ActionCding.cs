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
            context.cdTrigger.Restart();
        }


        public override void OnExit()
        {
            //Debug.Log("BatlleReady OnExit");
            base.OnExit();
        }

        public override void Update(BattleFrame frame)
        {
            base.Update(frame);

            context.cdTrigger.Update(frame);

            if (context.IsCDComplete())
            {
                //通知动作管理器，CD完成了
                //context.NotifyCdComplete();

                //设置触发器无效
                context.cdTrigger.SetInValid();

                //动作进入待机状态
                context.Standby();
            }
        }
    }
}