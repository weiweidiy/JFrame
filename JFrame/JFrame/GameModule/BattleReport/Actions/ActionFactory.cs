using System;
using System.Collections.Generic;

namespace JFrame
{

    public class ActionFactory
    {
        int unitId;
        ActionDataSource actionDataSource;
        BattlePoint battlePoint;
        PVPBattleManager pvpBattleManager;
        public ActionFactory(int unitId, ActionDataSource dataSource, BattlePoint battlePoint, PVPBattleManager pvpBattleManager)
        {
            this.unitId = unitId;
            this.actionDataSource = dataSource;
            this.battlePoint = battlePoint;
            this.pvpBattleManager = pvpBattleManager;
        }

        public IBattleAction Create(int actionId)
        {
            return new NormalAction(actionId,
                        CreateTrigger(actionDataSource.GetTriggerType(unitId, actionId), actionDataSource.GetTriggerArg(unitId, actionId), 0f)
                        , CreateTargetFinder(actionDataSource.GetFinderType(unitId, actionId), battlePoint, actionDataSource.GetFinderArg(unitId, actionId))
                        , CreateExecutors(unitId, actionId));
        }

        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="triggerType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        BattleTrigger CreateTrigger(int triggerType, float arg, float delay = 0)
        {
            switch (triggerType)
            {
                case 1: //周期性触发器
                    return new CDTrigger(arg, delay);
                default:
                    throw new Exception(triggerType + " 技能未实现的 trigger type " + triggerType);
            }
        }

        /// <summary>
        /// 创建目标搜索器
        /// </summary>
        /// <param name="finderType"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        IBattleTargetFinder CreateTargetFinder(int finderType, BattlePoint point, float arg)
        {
            switch (finderType)
            {
                case 1: //顺序找目标（可复数）
                    return new OrderTargetFinder(point, pvpBattleManager, arg);
                case 2: //倒序找目标（可复数）
                    return new ReverseOrderTargetFinder(point, pvpBattleManager, arg);
                default:
                    throw new Exception("没有实现目标finder type " + finderType);
            }
        }

        /// <summary>
        /// 创建执行器
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="actionId"></param>
        /// <returns></returns>
        List<IBattleExecutor> CreateExecutors(int unitId, int actionId)
        {
            var result = new List<IBattleExecutor>();
            var executors = actionDataSource.GetExcutorTypes(unitId, actionId);

            foreach (var executorId in executors)
            {
                var e = CreateExcutor( executorId, actionDataSource.GetExcutorArg(unitId, actionId, executorId));
                result.Add(e);
            }
            return result;
        }

        /// <summary>
        /// 创建执行器
        /// </summary>
        /// <param name="excutorType"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        IBattleExecutor CreateExcutor( int excutorType, float[] arg)
        {
            switch (excutorType)
            {
                case 1: //伤害执行器（可多段伤害）
                    return new BattleDamage(arg);
                case 2: //给目标添加buffer的执行器
                    return new BattleTargetAddBuffer(arg);
                default:
                    throw new Exception("没有实现指定的 excutor type " + excutorType);
            }
        }
    }
}