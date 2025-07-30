using System.Collections.Generic;

namespace JFramework.Game
{
    public abstract class JCombatTriggerBase: JCombatActionComponent,  IJCombatTrigger/*, IJCombatUnitEventListener, IJCombatTurnBasedEventListener*/
    {
        public event System.Action<IJCombatTrigger, IJCombatTriggerArgs> onTriggerOn;

        bool isTriggerOn = false;

        protected JCombatTriggerBase(float[] args) : base(args)
        {
        }

        public bool IsTriggerOn() => isTriggerOn;
        public void Reset() => isTriggerOn = false;
        public virtual void TriggerOn(IJCombatTriggerArgs triggerArgs)
        {
            isTriggerOn = true;
            onTriggerOn?.Invoke(this, triggerArgs);
        }

        //public virtual void OnBeforeHurt(IJCombatDamageData damageData) { }
        //public virtual void OnAfterDamage(IJCombatDamageData damageData) { }
        //public virtual void OnTurnStart(int frame) { }
        //public virtual void OnTurnEnd(int frame) { }


    }
}
