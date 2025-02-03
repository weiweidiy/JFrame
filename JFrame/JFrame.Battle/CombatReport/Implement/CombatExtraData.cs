using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 透传参数
    /// </summary>
    public class CombatExtraData
    {
        /// <summary>
        /// 屬性唯一id
        /// </summary>
        public string AttrUid { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public long Value { get; set; }

        /// <summary>
        /// 是否暴击
        /// </summary>
        public bool IsCri { get; set; }

        /// <summary>
        /// 是否格挡
        /// </summary>
        public bool IsBlock { get; set; }

        /// <summary>
        /// 是否抵消
        /// </summary>
        public bool IsGuard { get; set; }
        
        /// <summary>
        /// 是否免疫
        /// </summary>
        public bool IsImmunity { get; set; }

        /// <summary>
        /// 发起的单位
        /// </summary>
        public ICombatUnit SourceUnit { get; set; }

        /// <summary>
        /// 哪個aciton造成的
        /// </summary>
        public ICombatAction Action { get; set; }

        /// <summary>
        /// 目标单位
        /// </summary>
        public List<ICombatUnit> Targets { get; set; }

    }




}