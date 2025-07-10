using JFramework;
using JFramework.Game;
using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    public class JCombatUnit : RunableDictionaryContainer<IUnique>, IJCombatOperatable, IJCombatCastable
    {
        public string Uid { get; private set; }

        protected IJCombatAttrNameQuery combatAttrNameQuery;


        protected IJCombatEventListener eventListener;

        public JCombatUnit(string uid, List<IUnique> attrList,  Func<IUnique, string> keySelector, IJCombatAttrNameQuery combatAttrNameQuery,  IJCombatEventListener eventListener = null) : this(uid, attrList,keySelector, combatAttrNameQuery)
        {

            this.eventListener = eventListener;
        }

        public JCombatUnit(string uid, List<IUnique> attrList, Func<IUnique, string> keySelector, IJCombatAttrNameQuery combatAttrNameQuery) : base(keySelector)
        {
            AddRange(attrList);

            this.combatAttrNameQuery = combatAttrNameQuery;
            this.Uid = uid;
        }

        #region 可操作战斗属性接口
        public virtual IUnique GetAttribute(string uid)
        {
            var attr = Get(uid);
            return attr;
        }

        public bool IsDead()
        {
            var attr = GetAttribute(combatAttrNameQuery.GetHpAttrName()) as GameAttributeInt;

            if (attr == null)
                return true;

            return attr.CurValue <= 0;
        }

        public int OnDamage(IJCombatDamageData damageData)
        {
            var attrHp = Get(combatAttrNameQuery.GetHpAttrName()) as GameAttributeInt;
            var preValue = attrHp.CurValue;

            var damage = damageData.GetDamage();

            var curValue = attrHp.Minus(damage);

            eventListener?.OnDamage(damageData);

            return preValue - curValue;
        }
        #endregion

        #region 可释放技能接口
        public virtual void Cast() { }

        public virtual bool CanCast()
        {
            return !IsDead();
        }
        #endregion
    }
}
