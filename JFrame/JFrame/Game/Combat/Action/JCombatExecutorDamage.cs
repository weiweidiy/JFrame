using System.Collections.Generic;

namespace JFrame.Game
{
    public class JCombatExecutorDamage : JCombatExecutorBase
    {
        public JCombatExecutorDamage(IJCombatTargetsFinder finder) : base(finder)
        {
        }

        protected override void DoExecute(List<IJCombatUnit> finalTargets)
        {
            if(finalTargets != null)
            {
                foreach(var target in finalTargets)
                {
                    
                }
            }
        }
    }
}
