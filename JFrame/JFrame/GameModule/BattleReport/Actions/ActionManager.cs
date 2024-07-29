using System.Collections.Generic;
using System;

namespace JFrame
{
    /// <summary>
    /// 动作管理器
    /// </summary>
    public class ActionManager : BaseContainer<IBattleAction>, IActionManager
    {
        public override void Add(IBattleAction member)
        {
            base.Add(member);
            member.onTriggerOn += Member_onTriggerOn; 
            member.onStartCast += Member_onStartCast;
        }


        private void Member_onTriggerOn(IBattleAction arg1, List<IBattleUnit> arg2)
        {
            
        }

        private void Member_onStartCast(IBattleAction arg1, List<IBattleUnit> arg2)
        {
            
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