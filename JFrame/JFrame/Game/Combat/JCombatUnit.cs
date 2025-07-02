using JFramework;
using JFramework.Game;
using System;
using System.Collections.Generic;

namespace JFrame.Game
{

    public class JCombatUnit : DictionaryContainer<IUnique>, IJCombatUnit
    {
        public string Uid { get; set; }

        IJCombatAttrNameQuery combatAttrNameQuery;

        public JCombatUnit(List<IUnique> attrList,  Func<IUnique, string> keySelector, IJCombatAttrNameQuery combatAttrNameQuery) : base(keySelector)
        {
            AddRange(attrList);

            this.combatAttrNameQuery = combatAttrNameQuery;
        }    

        public IUnique GetAttribute(string uid)
        {
            var attr = Get(uid);
            return attr;
        }

        public bool IsDead()
        {
            var attr = Get(combatAttrNameQuery.GetHpAttrName()) as GameAttributeInt;

            if (attr == null)
                return true;

            return attr.CurValue <= 0;
        }
    }
}
