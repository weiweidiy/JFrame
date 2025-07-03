using JFramework;
using JFramework.Game;
using System;
using System.Collections.Generic;

namespace JFrame.Game
{
    public class JCombatTurnBasedUnit : JCombatUnit, IJTurnBasedCombatUnit
    {
        List<IJCombatAction> actions;
        public JCombatTurnBasedUnit(string uid, List<IUnique> attrList, Func<IUnique, string> keySelector, IJCombatTurnBasedAttrNameQuery combatAttrNameQuery, List<IJCombatAction> actions) : base(uid, attrList, keySelector, combatAttrNameQuery)
        {
            this.actions = actions;
        }

        public void Action(IJCombatQuery query)
        {
            if (actions == null)
                return;

            foreach(var action in actions)
            {
                action.Run(query);
            }
        }

        public bool CanAction()
        {
            return true;
        }

        public int GetActionPoint()
        {
            var query = combatAttrNameQuery as IJCombatTurnBasedAttrNameQuery;
            var attr = GetAttribute(query.GetActionPointName());
            var attrInt = attr as GameAttributeInt;
            return attrInt.CurValue;
        }
    }
}
