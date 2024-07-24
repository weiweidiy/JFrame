using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 自己队伍顺序伤员成员 ： type = 3
    /// </summary>
    public class OrderFriendsHurtFinder : BaseTargetFinder
    {
        public OrderFriendsHurtFinder(BattlePoint selfPoint, IPVPBattleManager manger, float arg) : base(selfPoint, manger, arg) { }

        public override List<IBattleUnit> FindTargets()
        {
            var result = new List<IBattleUnit>();

            var units = manger.GetUnits(selfPoint.Team);

            //debug
            foreach (var unit in units)
            {
                if (unit.IsAlive() && !unit.IsHpFull())
                {
                    result.Add(unit);
                    //有效数量校验
                    if (result.Count >= this.arg)
                        return result;
                }
            }

            return result;
        }
    }


}