using System;
using System.Collections.Generic;

namespace JFrame
{

    /// <summary>
    /// 觸發器基類
    /// </summary>
    public abstract class CombatBaseTrigger : BaseActionComponent, ICombatTrigger, IActionContent
    {
        /// <summary>
        /// 透傳參數
        /// </summary>
        protected CombatExtraData _extraData;
        public CombatExtraData ExtraData
        {
            get => _extraData; 
            set
            {
                _extraData = value;
            }
        }
     

        bool isOn;

        /// <summary>
        /// 查找器
        /// </summary>
        protected CombatBaseFinder finder;

        public CombatBaseTrigger(CombatBaseFinder finder)
        {
            this.finder = finder;
        }

        /// <summary>
        /// 查詢是否觸發
        /// </summary>
        /// <returns></returns>
        public bool IsOn()
        {
            return isOn;
        }

        /// <summary>
        /// 重置為未觸發
        /// </summary>
        public virtual void Reset()
        {
            SetOn(false);
        }

        /// <summary>
        /// 設置觸發狀態
        /// </summary>
        /// <param name="on"></param>
        public void SetOn(bool on)
        {
            isOn = on;
        }

        /// <summary>
        /// 設置透傳參數的源單位
        /// </summary>
        /// <param name="target"></param>
        public override void OnAttach(CombatAction target)
        {
            base.OnAttach(target);
        }

        protected override void OnUpdate(BattleFrame frame)
        {
        }
    }
}