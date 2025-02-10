using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 找单位的触发器 参数 0 ： 查找数量  1：队伍参数 0=友军 1=敌军 
    /// </summary>
    public abstract class TriggerFindUnits : CombatBaseTrigger
    {
        /// <summary>
        /// 獲取查找數量
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected int GetFindAmountArg()
        {
            return (int)GetCurArg(0);
        }

        /// <summary>
        /// 获取队伍参数
        /// </summary>
        /// <returns></returns>
        protected int GetTeamIdArg()
        {
            return (int)GetCurArg(1);
        }

        /// <summary>
        /// 获取目标队伍id
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected int GetTargetTeamId()
        {
            var teamArg = GetTeamIdArg();
            if (teamArg == 0) //找自己
                return context.CombatManager.GetFriendTeamId(_extraData.SourceUnit);

            if (teamArg == 1)
                return context.CombatManager.GetOppoTeamId(_extraData.SourceUnit);

            throw new Exception("teamArg 没有找到目标队伍id " + teamArg);
        }


        public override void Update(BattleFrame frame)
        {
            var targets = GetFirstTargetUnits(); //初选目标

            SortList(targets);

            var finalUnits = GetFinalUnits(targets);

            if (finalUnits != null && finalUnits.Count > 0)
            {
                _extraData.Targets = finalUnits;
                SetOn(true);
            }
        }

        /// <summary>
        /// 首次获取目标
        /// </summary>
        /// <returns></returns>
        protected List<CombatUnit> GetFirstTargetUnits()
        {
            var teamId = GetTargetTeamId();
            //獲取所有攻擊範圍内的單位
            var teamUnits = GetUnitsByTargetTeam(teamId);

            var result = new List<CombatUnit>();

            result.AddRange(teamUnits);

            return result;
        }

        /// <summary>
        /// 从队伍里找单位 子类筛选 比如要在射程内 等等
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        protected abstract List<CombatUnit> GetUnitsByTargetTeam(int teamId);

        /// <summary>
        /// 按条件第2次筛选
        /// </summary>
        /// <param name="lst"></param>
        protected abstract void SortList(List<CombatUnit> lst);

        /// <summary>
        /// 获取最终单位
        /// </summary>
        /// <param name="units"></param>
        /// <returns></returns>
        protected List<CombatUnit> GetFinalUnits(List<CombatUnit> units)
        {
            var reslut = new List<CombatUnit>();
            //保證不能超過數組長度
            var finalCount = Math.Min(units.Count, GetFindAmountArg());

            for (int i = 0; i < finalCount; i++)
            {
                reslut.Add(units[i]);
            }
            return reslut;
        }

    }
}