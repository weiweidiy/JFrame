using System;
using System.Collections.Generic;
using System.Reflection;

namespace JFrame
{
    public class CombatActionFactory
    {
        /// <summary>
        /// 創建action列表 , key:actionId
        /// </summary>
        /// <param name="actionsData"></param>
        /// <param name="owner"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<CombatAction> CreateUnitActions(Dictionary<int,ActionInfo> actionsInfo, CombatUnit owner, CombatContext context)
        {
            if (actionsInfo == null)
                return null;

            var result = new List<CombatAction>();
            foreach (var action in actionsInfo)
            {
                var actionId = action.Key;
                var actionData = action.Value;
                var actionType = actionData.type;
                var actionMode = actionData.mode;
                var actionUid = actionData.uid;

                var dic = actionData.componentInfo;
                var conditionTriggers = dic[ActionComponentType.ConditionTrigger]; //條件觸發器
                var finders = dic[ActionComponentType.Finder]; //查找器
                var delayTrigger = dic[ActionComponentType.DelayTrigger];//延遲觸發器(只能有1個)
                var executors = dic[ActionComponentType.Executor]; //執行器
                var cdTriggers = dic[ActionComponentType.CdTrigger]; //cd觸發器
                var unitAction = new CombatUnitAction();
                unitAction.OnAttach(owner);
                var sm = new CombatActionSM();
                sm.Initialize(unitAction);

                var finder = finders.Count > 0 ? finders[0] : null;

                unitAction.Initialize(actionId, actionUid, actionType, actionMode, CreateTriggers(conditionTriggers, context, unitAction)
                            , CreateTrigger(delayTrigger[0], context, unitAction)
                            , CreateExecutors(executors, CreateFinder(finder, context,unitAction), context, unitAction)
                            , CreateCdTriggers(cdTriggers, context, unitAction),sm);

                result.Add(unitAction);
            }
            return result;
        }

        /// <summary>
        /// 創建觸發器列表
        /// </summary>
        /// <param name="conditionTriggers"></param>
        /// <param name="finders"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private List<CombatBaseTrigger> CreateTriggers(List<ActionComponentInfo> conditionTriggers, CombatContext context, CombatAction owner)
        {
            var result = new List<CombatBaseTrigger>();

            foreach (var componentInfo in conditionTriggers)
            {
                var trigger = CreateTrigger(componentInfo, context, owner);
                result.Add(trigger);
            }

            return result;
        }

        /// <summary>
        /// 創建finder列表
        /// </summary>
        /// <param name="finders"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private List<ICombatFinder> CreateFinders(List<ActionComponentInfo> finders, CombatContext context, CombatAction owner)
        {
            var result = new List<ICombatFinder>();

            foreach (var componentInfo in finders)
            {
                var finder = CreateFinder(componentInfo, context, owner);
                result.Add(finder);
            }

            return result;
        }

        /// <summary>
        /// 創建執行器列表
        /// </summary>
        /// <param name="executors"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private List<CombatBaseExecutor> CreateExecutors(List<ActionComponentInfo> executors, ICombatFinder finder, CombatContext context, CombatAction owner)
        {
            var result = new List<CombatBaseExecutor>();

            foreach (var componentInfo in executors)
            {
                var executor = CreateExecutor(componentInfo,finder, context, owner);
                result.Add(executor);
            }

            return result;
        }

        /// <summary>
        /// 創建cd觸發器列表
        /// </summary>
        /// <param name="cdTriggers"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private List<CombatBaseTrigger> CreateCdTriggers(List<ActionComponentInfo> cdTriggers, CombatContext context, CombatAction owner)
        {
            var result = new List<CombatBaseTrigger>();

            foreach (var componentInfo in cdTriggers)
            {
                var trigger = CreateTrigger(componentInfo, context, owner);
                result.Add(trigger);
            }

            return result;
        }

        /// <summary>
        /// 創建觸發器
        /// </summary>
        /// <param name="componentInfo"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        CombatBaseTrigger CreateTrigger(ActionComponentInfo componentInfo, CombatContext context, CombatAction owner)
        {
            CombatBaseTrigger trigger = null;
            switch (componentInfo.id)
            {
                case 1:
                    {
                        trigger = new TriggerRangeNearest();
                    }
                    break;
                case 2:
                    {
                        trigger = new TriggerRangeFartest();
                    }
                    break;
                case 3:
                    {
                        trigger = new TriggerTime(); //時長觸發器
                    }
                    break;
                case 4:
                    {
                        trigger = new TriggerHp(); //hp百分比觸發器
                    }
                    break;
                default:
                    throw new NotImplementedException("沒有實現組件類型 " + componentInfo.id);
            }
            trigger.OnAttach(owner);
            trigger.Initialize(context, componentInfo.args);
            return trigger;
        }

        /// <summary>
        /// 創建finder
        /// </summary>
        /// <param name="componentInfo"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        ICombatFinder CreateFinder(ActionComponentInfo componentInfo, CombatContext context, CombatAction owner)
        {
            if (componentInfo == null)
                return null;

            ICombatFinder finder = null;
            switch (componentInfo.id)
            {
                case 1:
                    {
                        finder = new FinderOneOppo();
                    }
                    break;
                default:
                    throw new NotImplementedException("沒有實現組件類型 " + componentInfo.id);
            }
            (finder as BaseActionComponent).OnAttach(owner);
            (finder as BaseActionComponent).Initialize(context, componentInfo.args);
            return finder;
        }

        /// <summary>
        /// 創建執行器
        /// </summary>
        /// <param name="componentInfo"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        CombatBaseExecutor CreateExecutor(ActionComponentInfo componentInfo, ICombatFinder finder, CombatContext context, CombatAction owner)
        {
            CombatBaseExecutor executor = null;
            switch (componentInfo.id)
            {
                case 1:
                    {
                        executor = new ExecutorCombatDamage(finder);
                    }
                    break;
                case 2:
                    {
                        executor = new ExecutorCombatContinuousDamage(finder);
                    }           
                    break;
                case 3:
                    {
                        executor = new ExecutorCombatHeal(finder);
                    }
                    break;
                default:
                    throw new NotImplementedException("沒有實現組件類型 " + componentInfo.id);
            }
            executor.OnAttach(owner);
            executor.Initialize(context, componentInfo.args);
            return executor;
        }
    }
}