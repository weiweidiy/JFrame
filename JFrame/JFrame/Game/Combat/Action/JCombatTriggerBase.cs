using System.Collections.Generic;

namespace JFramework.Game
{
    public abstract class JCombatTriggerBase<T> : JCombatActionComponent,  IJCombatTrigger, IJCombatUnitEventListener, IJCombatTurnBasedEventListener
    {
        public event System.Action<object> onTriggerOn;

        bool isTriggerOn = false;
        public bool IsTriggerOn() => isTriggerOn;
        public void Reset() => isTriggerOn = false;
        public virtual void TriggerOn(T targets)
        {
            isTriggerOn = true;
            onTriggerOn?.Invoke(targets);
        }

        public virtual void OnBeforeDamage(IJCombatDamageData damageData) { }
        public virtual void OnAfterDamage(IJCombatDamageData damageData) { }
        public virtual void OnTurnStart(int frame) { }
        public virtual void OnTurnEnd(int frame) { }


    }
}
