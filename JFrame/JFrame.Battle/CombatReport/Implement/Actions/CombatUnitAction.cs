using System;

namespace JFrame
{
    /// <summary>
    /// unit上的action
    /// </summary>
    public class CombatUnitAction : CombatAction, ICombatAttachable<IActionContent>
    {
        public virtual IActionContent Owner { get; private set; }


        public void OnAttach(IActionContent ower)
        {
            Owner = ower;
        }

        public void OnDetach()
        {
            Owner = null;
        }
    }

}