using JFramework;
using JFramework.Game;
using System;
using System.Collections.Generic;

namespace JFrame.Game
{
    public class JCombatTurnBasedUnit : JCombatUnit, IJTurnBasedCombatUnit
    {
        public JCombatTurnBasedUnit(string uid, List<IUnique> attrList, Func<IUnique, string> keySelector, IJCombatTurnBasedAttrNameQuery combatAttrNameQuery) : base(uid, attrList, keySelector, combatAttrNameQuery)
        {

        }

        public void Action(IJCombatQuery query)
        {
            
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
