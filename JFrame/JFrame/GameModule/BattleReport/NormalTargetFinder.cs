using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 通过自身战位获取目标(先打前排)
    /// </summary>
    public class NormalTargetFinder : BattleTargetFinder
    {
        BattlePoint selfPoint;
        PVPBattleManager manger;

        public NormalTargetFinder(BattlePoint selfPoint, PVPBattleManager manger)
        {
            this.selfPoint = selfPoint;
            this.manger = manger;
        }

        /// <summary>
        /// 获取攻击目标
        /// </summary>
        /// <returns></returns>
        public override List<IBattleUnit> FindTargets()
        {
            var result = new List<IBattleUnit>();

            //foreach(var key in targetsPoint.Keys)
            //{
            //    //to do:获取随机前排，如果没有则随机获取后排
            //    IBattleUnit target = targetsPoint[key];
            //    result.Add(target);
            //}

            return result;
        }
    }
}