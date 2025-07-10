using JFramework;
using JFramework.Game;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatTurnBasedUnit : JCombatUnit, IJCombatTurnBasedUnit
    {
        List<IJCombatAction> actions;
        public JCombatTurnBasedUnit(string uid, List<IUnique> attrList, Func<IUnique, string> keySelector, IJCombatTurnBasedAttrNameQuery combatAttrNameQuery,  List<IJCombatAction> actions, IJCombatEventListener eventListener = null) 
            : base(uid, attrList, keySelector, combatAttrNameQuery, eventListener)
        {
            this.actions = actions;
            if(this.actions != null)
            {
                foreach(var  action in actions)
                    action.SetCaster(new JCombatUnitCasterQuery(this));
            }
        }

        protected override void OnStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);
            if (actions != null)
            {
                foreach (var action in actions)
                {
                    
                    action.OnStart(/*query*/);
                }
            }
        }

        protected override void OnUpdate(RunableExtraData extraData)
        {
            base.OnUpdate(extraData);      
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (actions != null)
            {
                foreach (var action in actions)
                {
                    action.OnStop();
                }
            }
        }



        public int GetActionPoint()
        {
            var query = combatAttrNameQuery as IJCombatTurnBasedAttrNameQuery;
            var attr = GetAttribute(query.GetActionPointName());
            var attrInt = attr as GameAttributeInt;
            return attrInt.CurValue;
        }

        public override void Cast()
        {
            base.Cast();

            if (actions == null)
                return;

            foreach (var action in actions)
            {
                action.Cast();
            }
        }
    }
}
