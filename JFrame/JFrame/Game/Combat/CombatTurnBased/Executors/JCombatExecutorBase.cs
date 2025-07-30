using System;
using System.Collections.Generic;

namespace JFramework.Game
{


    public abstract class JCombatExecutorBase : JCombatActionComponent, IJCombatExecutor
    {
        protected IJCombatTargetsFinder finder;

        protected IJCombatFormula formulua;

        protected JCombatTurnBasedEvent objEvent;

        public IJCombatFilter filter;

        public class ExecutorFilterArgs : IJCombatFilterArgs
        {

        }

        ExecutorFilterArgs executorFilterArgs = new ExecutorFilterArgs();

        public JCombatExecutorBase(IJCombatFilter filter, IJCombatTargetsFinder finder, IJCombatFormula formulua, float[] args = null) : base(args)
        {
            this.finder = finder;
            this.formulua = formulua;
            this.filter = filter;
        }


        public IJCombatExecutorArgs Execute(IJCombatTriggerArgs triggerArgs, IJCombatExecutorArgs executorArgs, IJCombatCasterTargetableUnit target)
        {
            var needExecutor = true;
            if(filter != null)
            {
                //executorFilterArgs.xx = triggerArgs;
                needExecutor = filter.Filter(executorFilterArgs);
            }
                

            if (needExecutor)
                return DoExecute(triggerArgs, executorArgs, target);
            else
                return executorArgs;
        }

        protected abstract IJCombatExecutorArgs DoExecute(IJCombatTriggerArgs triggerArgs, IJCombatExecutorArgs executorArgs, IJCombatCasterTargetableUnit target);

        public override void SetOwner(IJCombatAction owner)
        {
            base.SetOwner(owner);
            if (finder != null)
                finder.SetOwner(owner);

            if (formulua != null)
            {
                formulua.SetOwner(owner);
            }

            if(filter != null)
                filter.SetOwner(owner);
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

            if(filter != null)
                filter.SetQuery(query);
        }

        public void AddCombatEvent(JCombatTurnBasedEvent combatEvent)
        {
            objEvent = combatEvent;
        }

        protected override void OnStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);

            if (finder != null)
                finder.Start(extraData);

            if (formulua != null)
            {
                formulua.Start(extraData);
            }

            if (filter != null)
                filter.Start(extraData);
        }

        protected override void OnStop()
        {
            base.OnStop();

            if (finder != null)
                finder.Stop();

            if (formulua != null)
            {
                formulua.Stop();
            }

            if (filter != null)
                filter.Stop();
        }
    }
}
