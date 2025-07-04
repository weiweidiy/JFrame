using System;
using System.Collections.Generic;

namespace JFrame.Game
{
    /// <summary>
    /// 战斗行为基类，触发器触发执行，
    /// </summary>
    public abstract class JCombatActionBase : IJCombatAction
    {
        public string Uid { get; private set; }

        IJCombatQuery query;

        List<IJCombatTrigger> triggers;
        List<IJCombatExecutor> executors;

        public JCombatActionBase(string uid, List<IJCombatTrigger> triggers,  List<IJCombatExecutor> executors)
        {
            this.Uid = uid;
            this.triggers = triggers;
            this.executors = executors;
            if (triggers != null)
            {
                foreach (IJCombatTrigger trigger in triggers)
                {
                    //设置父节点
                }
            }

            if(executors != null)
            {
                foreach(IJCombatExecutor executor in executors)
                {
                    //设置父节点
                }
            }

        }

        public void Start(IJCombatQuery query)
        {
            this.query = query;

            if (triggers != null)
            {
                foreach (var trigger in triggers)
                {
                    trigger.OnStart(query);
                    trigger.onTriggerOn += Trigger_onTriggerOn;
                }
            }

            if(executors != null)
            {
                foreach(var executor in executors)
                {
                    executor.OnStart(query);
                }
            }
        }

        public void Stop()
        {
            if (triggers != null)
            {
                foreach (IJCombatTrigger trigger in triggers)
                {
                    trigger.OnStop();
                    trigger.onTriggerOn -= Trigger_onTriggerOn;
                }
            }

            if (executors != null)
            {
                foreach (var executor in executors)
                {
                    executor.OnStop();
                }
            }

        }

        private void Trigger_onTriggerOn(List<IJCombatUnit> targets)
        {
            Execute(targets);
        }

        public void Act()
        {
            Execute(null);
        }

        public void Execute(List<IJCombatUnit> targets)
        {
            if (executors != null)
            {
                foreach (var executor in executors)
                {
                    executor.Execute(targets);
                }
            }
        }
    }
}
