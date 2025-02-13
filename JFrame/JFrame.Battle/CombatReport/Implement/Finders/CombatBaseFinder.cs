using JFrame.Common;
using System;
using System.Collections.Generic;

namespace JFrame
{

    public abstract class CombatBaseFinder : BaseActionComponent, ICombatFinder
    {
        public abstract List<CombatUnit> FindTargets(CombatExtraData extraData);

        protected override void OnUpdate(ComabtFrame frame)
        {
            //throw new NotImplementedException();
        }
    }
}