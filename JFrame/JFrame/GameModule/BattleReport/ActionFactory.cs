using System;
using System.Collections.Generic;

namespace JFrame
{
    public class ActionFactory
    {
        int unitId;
        DataSource dataSource;
        BattlePoint battlePoint;
        PVPBattleManager pvpBattleManager;
        public ActionFactory(int unitId, DataSource dataSource, BattlePoint battlePoint, PVPBattleManager pvpBattleManager)
        {
            this.unitId = unitId;
            this.dataSource = dataSource;
            this.battlePoint = battlePoint;
            this.pvpBattleManager = pvpBattleManager;
        }

        public IBattleAction Create(int actionId)
        {
            return new NormalAction(actionId,
                        CreateTrigger(dataSource.GetTriggerType(unitId, actionId), dataSource.GetTriggerArg(unitId, actionId), 0f)
                        , CreateTargetFinder(dataSource.GetFinderType(unitId, actionId), battlePoint, dataSource.GetFinderArg(unitId, actionId))
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
                case 1:
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
                case 1:
                    return new NormalTargetFinder(point, pvpBattleManager, arg);
                default:
                    throw new Exception("没有实现目标finder type " + finderType);
            }
        }

        List<IBattleExecutor> CreateExecutors(int unitId, int actionId)
        {
            var result = new List<IBattleExecutor>();
            var executors = dataSource.GetExcutorType(unitId, actionId);

            foreach (var executorId in executors)
            {
                var e = CreateExcutor( executorId, dataSource.GetExcutorArg(unitId, actionId, executorId));
                result.Add(e);
            }
            return result;
        }

        IBattleExecutor CreateExcutor( int excutorType, float[] arg)
        {
            switch (excutorType)
            {
                case 1:
                    return new BattleDamage(arg);
                default:
                    throw new Exception("没有实现指定的 excutor type " + excutorType);
            }
        }
    }
}