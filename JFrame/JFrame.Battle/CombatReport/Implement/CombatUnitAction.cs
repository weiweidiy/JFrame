using System;

namespace JFrame
{
    /// <summary>
    /// unit上的action
    /// </summary>
    public class CombatUnitAction : CombatAction, IAttachable<ICombatUnit>
    {
        public ICombatUnit Owner => throw new NotImplementedException();

        public void OnAttach(ICombatUnit target)
        {
            throw new NotImplementedException();
        }

        public void OnDetach()
        {
            throw new NotImplementedException();
        }
    }

}