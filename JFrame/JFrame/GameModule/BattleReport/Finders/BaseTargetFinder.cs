﻿using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 搜索器基类
    /// </summary>
    public abstract class BaseTargetFinder : IBattleTargetFinder
    {
        protected BattlePoint selfPoint;
        protected IPVPBattleManager manger;
        protected float arg;

        public BaseTargetFinder(BattlePoint selfPoint, IPVPBattleManager manger, float arg)
        {
            this.selfPoint = selfPoint;
            this.manger = manger;
            this.arg = arg;
        }

        public IBattleAction Owner { get; private set; }

        public abstract List<IBattleUnit> FindTargets();

        public void OnAttach(IBattleAction action)
        {
            Owner = action;
        }
    }




}