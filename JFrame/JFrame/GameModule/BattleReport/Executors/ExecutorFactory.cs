using System;

namespace JFrame
{
    public class ExecutorFactory
    {
        /// <summary>
        /// 创建执行器
        /// </summary>
        /// <param name="excutorType"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IBattleExecutor Create(FormulaManager formulaManager, int excutorType, float[] arg)
        {
            switch (excutorType)
            {
                case 1: //按释放者攻击力对目标伤害（可多段伤害）
                    return new ExecutorDamage(formulaManager, arg);
                case 2: //给目标添加buffer
                    return new ExecutorTargetAddBuffer(formulaManager, arg);
                case 3: //给目标回血（加值）
                    return new ExecutorHeal(formulaManager, arg);
                case 4: //按目标血量百分比伤害
                    return new ExecutorHpDamage(formulaManager, arg);
                case 5://提升生命上限
                    return new ExecutorMaxHpUp(formulaManager, arg);
                case 6://自己添加buffer
                    return new ExecutorSelfAddBuffer(formulaManager, arg);
                case 7://递增伤害
                    return new ExecutorIncrementalDamage(formulaManager, arg);
                case 8://复活
                    return new ExecutorReborn(formulaManager, arg);
                case 9: //吸血
                    return new ExecutorSuckHp(formulaManager, arg);
                case 10://属性变更
                    return new ExecutorAttrChange(formulaManager, arg);
                case 11://抵挡伤害
                    return new ExecutorShield(formulaManager, arg);
                case 12://打断
                    return new ExecutorControlStatus(formulaManager, arg);
                default:
                    throw new Exception("没有实现指定的 excutor type " + excutorType);
            }
        }
    }

    //public class ExecutorFactory
    //{
    //    /// <summary>
    //    /// 创建执行器
    //    /// </summary>
    //    /// <param name="excutorType"></param>
    //    /// <param name="arg"></param>
    //    /// <returns></returns>
    //    /// <exception cref="Exception"></exception>
    //    public IBattleExecutor Create(FormulaManager formulaManager, int excutorType, float[] arg)
    //    {
    //        switch (excutorType)
    //        {
    //            case 1: //按释放者攻击力对目标伤害（可多段伤害）
    //                return new ExecutorDamage(formulaManager, arg);
    //            case 2: //给目标添加buffer
    //                return new ExecutorTargetAddBuffer(formulaManager, arg);
    //            case 3: //给目标回血（加值）
    //                return new ExecutorHeal(formulaManager, arg);
    //            case 4: //按目标血量百分比伤害
    //                return new ExecutorHpDamage(formulaManager, arg);
    //            case 5://提升生命上限
    //                return new ExecutorMaxHpUp(formulaManager, arg);
    //            case 6://自己添加buffer
    //                return new ExecutorSelfAddBuffer(formulaManager, arg);
    //            case 7://递增伤害
    //                return new ExecutorIncrementalDamage(formulaManager, arg);
    //            case 8://复活
    //                return new ExecutorReborn(formulaManager, arg);
    //            case 9: //吸血
    //                return new ExecutorSuckHp(formulaManager, arg);
    //            default:
    //                throw new Exception("没有实现指定的 excutor type " + excutorType);
    //        }
    //    }
    //}

}