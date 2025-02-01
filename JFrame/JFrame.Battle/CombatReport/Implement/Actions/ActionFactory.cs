using System;
using System.Collections.Generic;

namespace JFrame
{
    public class CombatAttributeFactory
    {
        /// <summary>
        /// 創建所有屬性對象
        /// </summary>
        /// <param name="unitInfo"></param>
        /// <returns></returns>
        public List<IUnique> CreateAllAttributes(CombatUnitInfo unitInfo)
        {
            var result = new List<IUnique>();
            var hp = new CombatAttributeLong(PVPAttribute.HP.ToString(), unitInfo.hp, unitInfo.maxHp);
            var atk = new CombatAttributeLong(PVPAttribute.ATK.ToString(), unitInfo.atk, long.MaxValue);
            var atkSpeed = new CombatAttributeDouble(PVPAttribute.AtkSpeed.ToString(), unitInfo.atkSpeed, double.MaxValue);
            var cri = new CombatAttributeDouble(PVPAttribute.Critical.ToString(), unitInfo.cri, double.MaxValue);
            var criDmgRate = new CombatAttributeDouble(PVPAttribute.CriticalDamage.ToString(), unitInfo.criDmgRate, double.MaxValue);
            var criDmgAnti = new CombatAttributeDouble(PVPAttribute.CriticalDamageResist.ToString(), unitInfo.criDmgAnti, double.MaxValue); //暴击伤害抵抗百分比
                                                                                                                                            //public float skillDmgRate; //技能伤害加成百分比
                                                                                                                                            //public float skillDmgAnti; //技能伤害抵抗百分比
                                                                                                                                            //public float dmgRate; //伤害加成百分比
                                                                                                                                            //public float dmgAnti; //伤害抵抗百分比
                                                                                                                                            //public float debuffHit; //0~1异常状态命中百分比
                                                                                                                                            //public float debuffAnti; //0~1异常状态抵抗百分比
                                                                                                                                            //public float penetrate; //穿透 0~1 百分比
                                                                                                                                            //public float block;     //格挡 0~1 百分比
                                                                                                                                            //to do : 其他屬性
            result.Add(hp);
            result.Add(atk);
            result.Add(atkSpeed);
            result.Add(cri);
            result.Add(criDmgRate);
            result.Add(criDmgAnti);
            return result;
        }
    }
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
        /// 
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

        private List<ICombatTrigger> CreateCdTriggers(List<ActionComponentInfo> cdTriggers, CombatContext context)
        {
            throw new NotImplementedException();
        }

        private List<ICombatExecutor> CreateExecutors(List<ActionComponentInfo> executors, CombatContext context)
        {
            throw new NotImplementedException();
        }

        private List<ICombatFinder> CreateFinders(List<ActionComponentInfo> finders, CombatContext context)
        {
            throw new NotImplementedException();
        }
    }
}