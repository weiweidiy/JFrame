using System;
using System.Collections.Generic;
using System.Xml;

namespace JFrame
{
    public class BufferFactory
    {
        BufferDataSource dataSource;// = new BufferDataSource();
        PVPBattleManager pvpBattleManager;
        BattlePoint point;
        FormulaManager formulaManager;

        TriggerFactory conditionTriggerFactory = new TriggerFactory();
        FinderFactory finderFactory = new FinderFactory();
        ExecutorFactory executorFactory = new ExecutorFactory();


        public BufferFactory(BufferDataSource dataSource, PVPBattleManager pvpBattleManager, BattlePoint point, FormulaManager formulaManager )
        {
            this.dataSource = dataSource;
            this.pvpBattleManager = pvpBattleManager;
            this.point = point;
            this.formulaManager = formulaManager;
        }

        public virtual Buffer Create(IBattleUnit caster, int buffId, int foldCount = 1)
        {
            var trigger = conditionTriggerFactory.Create(pvpBattleManager, dataSource.GetConditionTriggerType(buffId), new float[1] { dataSource.GetConditionTriggerType(buffId) }, 0f);
            var finder = finderFactory.Create(pvpBattleManager, dataSource.GetFinderType(buffId), point, dataSource.GetFinderType(buffId));
            var executors = CreateExecutors(buffId);
            switch (buffId)
            {
                case 101: //增加攻速
                    return new BufferAttackSpeedUp(caster, Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId),trigger, finder, executors);
                case 102:
                    return new DurationBuffer(caster, Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId), trigger, finder, executors);
                case 103: //增加状态抵抗
                    return new DurationBuffer(caster, Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId), trigger, finder, executors);
                case 104:
                    return new DurationBuffer(caster, Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId), trigger, finder, executors);
                case 201:
                    return new DurationBuffer(caster, Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId), trigger, finder, executors);
                case 202:
                    return new DurationBuffer(caster, Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId), trigger, finder, executors);
                case 203:
                    return new DurationBuffer(caster, Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId), trigger, finder, executors);
                case 998:
                    return new DebufferAttackSpeedDown(caster, Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId), trigger, finder, executors);
                case 999:
                    return new DurationBuffer(caster, Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId), trigger, finder, executors);
                default:
                    throw new Exception("没有实现指定的技能buff " + buffId);
            }
        }

        private List<IBattleExecutor> CreateExecutors(int buffId)
        {
            var result = new List<IBattleExecutor>();
            var executors = dataSource.GetExcutorTypes(buffId);

            //foreach (var executorId in executors)
            for (int i = 0; i < executors.Count; i++)
            {
                var executorId = executors[i];
                var e = executorFactory.Create(formulaManager, executorId, dataSource.GetExcutorArg(buffId, i));   //CreateExcutor(executorId, actionDataSource.GetExcutorArg(unitUID, unitId, actionId, i));
                result.Add(e);
            }
            return result; ;
        }
    }
}


//var type = Type.GetType("JFrame.BufferAttackDown");
//object[] args = new object[4] { Guid.NewGuid().ToString(), buffId, foldCount, dataSource.GetArgs(buffId) };
//var buff = (Buffer) Activator.CreateInstance(type, args);
//return buff;
