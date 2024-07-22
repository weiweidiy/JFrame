using System.Collections.Generic;

namespace JFrame
{
    public class ReverseOrderTargetFinder : BaseTargetFinder
    {
        public ReverseOrderTargetFinder(BattlePoint selfPoint, PVPBattleManager manger, float arg):base(selfPoint, manger, arg) { }

        /// <summary>
        /// 获取攻击目标
        /// </summary>
        /// <returns></returns>
        public override List<IBattleUnit> FindTargets()
        {
            var result = new List<IBattleUnit>();

            var units = manger.GetUnits(manger.GetOppoTeam(selfPoint.Team));

            //debug
            for(int i = units.Count - 1; i >= 0; i--)
            {
                var unit = units[i];

                if (unit.IsAlive())
                {
                    result.Add(unit);
                    if (result.Count >= this.arg)
                        return result;
                }
            }

            return result;
        }
    }


}