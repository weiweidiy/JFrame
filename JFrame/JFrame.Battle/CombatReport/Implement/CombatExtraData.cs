using System;
using System.Collections.Generic;

namespace JFrame
{
    public class  ReportData
    {
        public string SourceUnitUid { get;  set; }

        public int ActionId { get; set; }

        public string ActionUid { get; set; }
        public List<string> TargetsUid { get; set; }
        public string TargetUid { get; set; }

        public float CastDuration { get; set; }
        public float CdDuration { get; set; }

        public long Value { get; set; }

        public long TargetHp { get; set; }
        public long TargetMaxHp { get; set; }

        /// <summary>
        /// 是否暴击
        /// </summary>
        public bool IsCri { get; set; }

        /// <summary>
        /// 是否格挡
        /// </summary>
        public bool IsBlock { get; set; }
    }
    /// <summary>
    /// 透传参数
    /// </summary>
    public class CombatExtraData : ICloneable
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
        public CombatUnit SourceUnit { get; set; }

        /// <summary>
        /// 哪個aciton造成的
        /// </summary>
        public CombatAction Action { get; set; }

        /// <summary>
        /// 目标单位
        /// </summary>
        public List<CombatUnit> Targets { get; set; }

        /// <summary>
        /// 单一目标
        /// </summary>
        public CombatUnit Target { get; set; }

        /// <summary>
        /// 释放时长
        /// </summary>
        public float CastDuration { get; set; } 

        /// <summary>
        /// cd时长
        /// </summary>
        public float CdDuration { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public List<string> GetTargetsUid()
        {
            var result = new List<string>();

            foreach(var target in Targets)
            {
                result.Add(target.Uid);
            }

            return result;
        }
    }




}