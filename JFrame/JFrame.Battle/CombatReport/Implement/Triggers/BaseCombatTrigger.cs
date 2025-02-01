using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 觸發器基類
    /// </summary>
    public abstract class BaseCombatTrigger : BaseActionComponent, ICombatTrigger
    {
        public event Action<CombatExtraData> onTriggerOn;

        /// <summary>
        /// 查找器
        /// </summary>
        protected List<ICombatFinder> finders;

        /// <summary>
        /// 透傳參數
        /// </summary>
        protected CombatExtraData extraData;

        public BaseCombatTrigger(List<ICombatFinder> finders)
        {
            extraData = new CombatExtraData();
            this.finders = finders; 
        }

        public override void Update(BattleFrame frame)
        {
            if(finders == null)
                return;

            foreach (ICombatFinder finder in finders)
            {
                var targets = finder.FindTargets(extraData);
                if (targets != null && targets.Count > 0)
                {
                    extraData.Targets = targets;
                    onTriggerOn?.Invoke(extraData);
                }
            }
        }
    }
}