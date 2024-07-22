using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 搜索器基类
    /// </summary>
    public abstract class BaseTargetFinder : IBattleTargetFinder
    {
        protected BattlePoint selfPoint;
        protected PVPBattleManager manger;
        protected float arg;

        public BaseTargetFinder(BattlePoint selfPoint, PVPBattleManager manger, float arg)
        {
            this.selfPoint = selfPoint;
            this.manger = manger;
            this.arg = arg;
        }
        public abstract List<IBattleUnit> FindTargets();
    }




}