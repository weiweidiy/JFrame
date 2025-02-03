using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 觸發器基類
    /// </summary>
    public abstract class BaseTrigger : BaseActionComponent, ICombatTrigger
    {
        public event Action<CombatExtraData> onTriggerOn;

        protected void NotifyTriggerOn(CombatExtraData extraData) { onTriggerOn?.Invoke(extraData); }

        /// <summary>
        /// 查找器
        /// </summary>
        protected List<ICombatFinder> finders;

        /// <summary>
        /// 透傳參數
        /// </summary>
        protected CombatExtraData extraData;

        public BaseTrigger(List<ICombatFinder> finders)
        {
            extraData = new CombatExtraData();
            this.finders = finders; 
        }


    }
}