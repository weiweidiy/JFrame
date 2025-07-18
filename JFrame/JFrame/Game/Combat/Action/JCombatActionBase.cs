﻿using System;
using System.Collections.Generic;

namespace JFramework.Game
{
    /// <summary>
    /// 战斗行为基类，触发器触发执行，
    /// </summary>
    public abstract class JCombatActionBase : BaseRunable, IJCombatAction
    {
        public string Uid { get; private set; }

        IJCombatQuery query;

        List<IJCombatTrigger> triggers;
        List<IJCombatExecutor> executors;

        IJCombatCaster casterQuery;
        public JCombatActionBase(IJCombatQuery query, string uid, List<IJCombatTrigger> triggers,  List<IJCombatExecutor> executors)
        {
            this.Uid = uid;
            this.triggers = triggers;
            this.executors = executors;
            this.query = query;

            if (triggers != null)
            {
                foreach (IJCombatTrigger trigger in triggers)
                {
                    //设置父节点
                    trigger.SetOwner(this);
                }
            }

            if(executors != null)
            {
                foreach(IJCombatExecutor executor in executors)
                {
                    //设置父节点
                    executor.SetOwner(this);
                }
            }

        }

        protected override void OnStart(RunableExtraData extraData)
        {
            base.OnStart(extraData);

            if (triggers != null)
            {
                foreach (var trigger in triggers)
                {
                    trigger.Start(extraData);
                    trigger.onTriggerOn += Trigger_onTriggerOn;
                }
            }

            if(executors != null)
            {
                foreach(var executor in executors)
                {
                    executor.Start(extraData);
                }
            }
        }

        protected override void OnStop()
        {
            base.OnStop();

            if (triggers != null)
            {
                foreach (IJCombatTrigger trigger in triggers)
                {
                    trigger.Stop();
                    trigger.onTriggerOn -= Trigger_onTriggerOn;
                }
            }

            if (executors != null)
            {
                foreach (var executor in executors)
                {
                    executor.Stop();
                }
            }
        }

        private void Trigger_onTriggerOn(List<IJCombatCasterTargetableUnit> targets)
        {
            Execute(targets);
        }


        public void Execute(List<IJCombatCasterTargetableUnit> targets)
        {
            if (executors != null)
            {
                foreach (var executor in executors)
                {
                    executor.Execute(targets);
                }
            }
        }

        /// <summary>
        /// 设置actin释放者查询器
        /// </summary>
        /// <param name="casterQuery"></param>
        public void SetCaster(IJCombatCaster casterQuery) => this.casterQuery = casterQuery;

        /// <summary>
        /// 获取action释放者uid
        /// </summary>
        /// <returns></returns>
        public string GetCaster() => casterQuery.Uid;

        public bool CanCast()
        {
            throw new NotImplementedException();
        }

        public void Cast()
        {
            Execute(null);
        }
    }
}
