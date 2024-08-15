using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 动作释放时触发 参数1：目标actionID type = 5
    /// </summary>
    public class ActionCastTrigger : BaseBattleTrigger
    {
        int targetAciontId;
        public ActionCastTrigger(IPVPBattleManager battleManager, float[] args, float delay = 0) : base(battleManager, args, delay)
        {
            if (args.Length < 1)
                throw new Exception("ActionCastTrigger 需要1个参数");

            targetAciontId = (int)args[0];
        }

        public override void OnAttach(IBattleAction action)
        {
            base.OnAttach(action);

            var targetAction = action.Owner.GetAction(targetAciontId);

            if (targetAction == null)
                throw new Exception("没有找到目标 action " + targetAciontId);

            targetAction.onStartCast += Action_onStartCast;
        }

        private void Action_onStartCast(IBattleAction action, List<IBattleUnit> targets, float duration)
        {
            SetOn(true);
        }

    }

    public class NewActionCastTrigger : NewBattleTrigger 
    {
        int targetAciontId;
        public NewActionCastTrigger(IPVPBattleManager battleManager, float[] args, float delay = 0) : base(battleManager, args, delay)
        {
            if (args.Length < 1)
                throw new Exception("ActionCastTrigger 需要1个参数");

            targetAciontId = (int)args[0];
        }

        public override void OnAttach(IAttachOwner owner)
        {
            base.OnAttach(owner);

            var o = owner as IBattleAction;
            if (o == null)
                throw new Exception("attach owner 转换失败 ");

            var targetAction = o.Owner.GetAction(targetAciontId);

            if (targetAction == null)
                throw new Exception("没有找到目标 action " + targetAciontId);

            targetAction.onStartCast += Action_onStartCast;
        }

        private void Action_onStartCast(IBattleAction action, List<IBattleUnit> targets, float duration)
        {
            SetOn(true);
        }

    }
}