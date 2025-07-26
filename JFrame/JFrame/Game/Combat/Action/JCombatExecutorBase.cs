using System.Collections.Generic;

namespace JFramework.Game
{
    public abstract class JCombatExecutorBase : JCombatActionComponent, IJCombatExecutor
    {
        IJCombatTargetsFinder finder;

        protected IJCombatFormula formulua;

        public JCombatExecutorBase(IJCombatTargetsFinder finder, IJCombatFormula formulua)
        {
            this.finder = finder;
            this.formulua = formulua;
        }

        public void Execute(object args)
        {
            List<IJCombatCasterTargetableUnit> finalTargets = null;
            if (finder != null)
            {
                finalTargets = finder.GetTargets();
            }

            DoExecute(args, finalTargets);
        }

        protected abstract void DoExecute(object triggerArgs, List<IJCombatCasterTargetableUnit> finderTargets);

        public override void SetOwner(IJCombatAction owner)
        {
            base.SetOwner(owner);
            if (finder != null)
                finder.SetOwner(owner);

            if (formulua != null)
            {
                formulua.SetOwner(owner);
            }
        }

        public override void SetQuery(IJCombatQuery query)
        {
            base.SetQuery(query);

            if (finder != null)
                finder.SetQuery(query);

            if (formulua != null)
            {
                formulua.SetQuery(query);
            }
        }
    }
}
