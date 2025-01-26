using System;

namespace JFrame
{
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