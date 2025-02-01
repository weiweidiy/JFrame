using System;

namespace JFrame
{
    /// <summary>
    /// unit上的action
    /// </summary>
    public class CombatUnitAction : CombatAction, IAttachable<CombatUnit>
    {
        public virtual CombatUnit Owner { get; private set; }

        public void OnAttach(CombatUnit ower)
        {
            Owner = ower;
        }

        public void OnDetach()
        {
            Owner = null;
        }
    }

}