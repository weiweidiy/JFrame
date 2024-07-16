using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 通过自身战位获取目标(先打前排),只找一个目标
    /// </summary>
    public class NormalTargetFinder : IBattleTargetFinder
    {
        BattlePoint selfPoint;
        PVPBattleManager manger;
        float arg;

        public NormalTargetFinder(BattlePoint selfPoint, PVPBattleManager manger, float arg)
        {
            this.selfPoint = selfPoint;
            this.manger = manger;
            this.arg = arg;
        }

        /// <summary>
        /// 获取攻击目标
        /// </summary>
        /// <returns></returns>
        public List<IBattleUnit> FindTargets()
        {
            var result = new List<IBattleUnit>();

            var units = manger.GetUnits(manger.GetOppoTeam(selfPoint.Team));

            //debug
            foreach(var unit in units)
            {
                if(unit.IsAlive())
                {
                    result.Add(unit);
                    if(result.Count >= this.arg)
                        return result; 
                }                 
            }

            return result;
        }


    }
}