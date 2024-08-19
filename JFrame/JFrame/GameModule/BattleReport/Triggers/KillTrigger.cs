

using System.Collections.Generic;
using System;

namespace JFrame
{
    /// <summary>
    /// type 12
    /// </summary>
    public class KillTrigger : BaseBattleTrigger
    {
        protected int targetAciontId;

        protected IBattleAction targetAction;
        public KillTrigger(IPVPBattleManager battleManager, float[] args, float delay = 0) : base(battleManager, args, delay) {
            if (args.Length != 1)
                throw new Exception("KillTrigger 需要1个参数");

            targetAciontId = (int)args[0];
        }


        public override void OnAttach(IAttachOwner owner)
        {
            base.OnAttach(owner);

            var o = owner as IBattleAction;
            if (o == null)
                throw new Exception("attach owner 转换失败 ");

            targetAction = o.Owner.GetAction(targetAciontId);

            if (targetAction == null)
                throw new Exception("没有找到目标 action " + targetAciontId);

            targetAction.onHittedComplete += Action_onHittedComplete;
        }

        public override void OnDetach()
        {
            base.OnDetach();

            if (targetAction != null)
                targetAction.onHittedComplete -= Action_onHittedComplete;
        }

        protected virtual void Action_onHittedComplete(IBattleAction action, IBattleUnit caster,ExecuteInfo info, IBattleUnit target)
        {
            if(!target.IsAlive())
            {
                NotifyTriggerOn(this, action);
                SetOn(true);
            }

        }

    }
}