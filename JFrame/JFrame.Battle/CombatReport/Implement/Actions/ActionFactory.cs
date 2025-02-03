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
        public List<CombatAction> CreateActions(Dictionary<int, Dictionary<ActionComponentType, List<ActionComponentInfo>>> actionsData, CombatUnit owner, CombatContext context)
        {
            if (actionsData == null)
                return null;

            var result = new List<CombatAction>();
            foreach (var action in actionsData)
            {
                var actionId = action.Key;
                var dic = action.Value;
                var conditionTriggers = dic[ActionComponentType.ConditionTrigger]; //條件觸發器
                var finders = dic[ActionComponentType.Finder]; //查找器
                var executors = dic[ActionComponentType.Executor]; //執行器
                var cdTriggers = dic[ActionComponentType.CdTrigger]; //cd觸發器
                var unitAction = new CombatUnitAction();
                unitAction.OnAttach(owner);
                unitAction.Initialize(CreateTriggers(conditionTriggers, CreateFinders(finders, context), context), CreateExecutors(executors, context), CreateCdTriggers(cdTriggers, context));

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
        private List<ICombatTrigger> CreateTriggers(List<ActionComponentInfo> conditionTriggers, List<ICombatFinder> finders, CombatContext context)
        {
            var result = new List<ICombatTrigger>();

            foreach (var componentInfo in conditionTriggers)
            {
                var trigger = CreateTrigger(componentInfo, finders, context);
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
        private List<ICombatFinder> CreateFinders(List<ActionComponentInfo> finders, CombatContext context)
        {
            var result = new List<ICombatFinder>();

            foreach (var componentInfo in finders)
            {
                var finder = CreateFinder(componentInfo, context);
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
        private List<ICombatExecutor> CreateExecutors(List<ActionComponentInfo> executors, CombatContext context)
        {
            var result = new List<ICombatExecutor>();

            foreach (var componentInfo in executors)
            {
                var executor = CreateExecutor(componentInfo, context);
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
        private List<ICombatTrigger> CreateCdTriggers(List<ActionComponentInfo> cdTriggers, CombatContext context)
        {
            var result = new List<ICombatTrigger>();

            foreach (var componentInfo in cdTriggers)
            {
                var trigger = CreateTrigger(componentInfo, null, context);
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
        ICombatTrigger CreateTrigger(ActionComponentInfo componentInfo, List<ICombatFinder> finders, CombatContext context)
        {
            ICombatTrigger trigger = null;
            switch (componentInfo.id)
            {
                case 1:
                    {
                        trigger = new TriggerNone(finders);
                    }
                    break;
                default:
                    throw new NotImplementedException("沒有實現組件類型 " + componentInfo.id);
            }
            (trigger as BaseActionComponent).Initialize(context, componentInfo.args);
            return trigger;
        }

        /// <summary>
        /// 創建finder
        /// </summary>
        /// <param name="componentInfo"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        ICombatFinder CreateFinder(ActionComponentInfo componentInfo, CombatContext context)
        {
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
        ICombatExecutor CreateExecutor(ActionComponentInfo componentInfo, CombatContext context)
        {
            ICombatExecutor executor = null;
            switch (componentInfo.id)
            {
                case 1:
                    {
                        executor = new ExecutorCombatDamage();
                    }
                    break;
                default:
                    throw new NotImplementedException("沒有實現組件類型 " + componentInfo.id);
            }
            (executor as BaseActionComponent).Initialize(context, componentInfo.args);
            return executor;
        }
    }
}