using System.Collections.Generic;
using System;
using static System.Collections.Specialized.BitVector32;

namespace JFrame
{
    /// <summary>
    /// 动作管理器
    /// </summary>
    public class ActionManager : BaseContainer<IBattleAction>, IActionManager
    {
        //event Action<IBattleAction, List<IBattleUnit>> onTriggerOn;

        event Action<IBattleAction, List<IBattleUnit>> onStartCast;

        public override void Add(IBattleAction member)
        {
            base.Add(member);
            member.onTriggerOn += Member_onTriggerOn; 
            member.onStartCast += Member_onStartCast;
        }


        private void Member_onTriggerOn(IBattleAction action, List<IBattleUnit> targets)
        {
            //onTriggerOn?.Invoke(action, targets);

            action.Cast(action.Owner, targets);
        }

        private void Member_onStartCast(IBattleAction action, List<IBattleUnit> targets)
        {
            onStartCast?.Invoke(action,targets);
        }

        public override bool Remove(string uid)
        {
            var member = base.Get(uid);
            if (member != null)
            {
                member.onTriggerOn -= Member_onTriggerOn;
                member.onStartCast -= Member_onStartCast;
                return base.Remove(uid);
            }
            else
                return false;
        }

        public void Update(BattleFrame frame)
        {
            var actions = GetAll();
            foreach (var action in actions)
            {
                action.Update(frame);
            }
        }
    }
}