using System;
using System.Collections.Generic;

namespace JFrame
{
    [Serializable]
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

        /// <summary>
        /// 移动速度
        /// </summary>
        public CombatVector Velocity { get; set; }
    }




}