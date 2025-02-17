﻿using System;
using System.Collections.Generic;

namespace JFrame
{
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
        public double Value { get; set; }

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
        /// 持有者
        /// </summary>
        public virtual CombatUnit Owner { get; set; }

        /// <summary>
        /// 释放者
        /// </summary>
        public CombatUnit Caseter { get; set; }

        /// <summary>
        /// 哪個aciton造成的
        /// </summary>
        public CombatAction Action { get; set; }

        /// <summary>
        /// 目标单位
        /// </summary>
        public virtual List<CombatUnit> Targets { get; set; }

        /// <summary>
        /// 单一目标
        /// </summary>
        public CombatUnit Target { get; set; }

        /// <summary>
        /// ActionOwner
        /// </summary>
        //public virtual IActionOwner Owner { get; set; }

        /// <summary>
        /// 释放时长
        /// </summary>
        public float CastDuration { get; set; } 

        /// <summary>
        /// cd时长
        /// </summary>
        public float CdDuration { get; set; }

        /// <summary>
        /// 移动速度
        /// </summary>
        public CombatVector Velocity { get; set; }  

        /// <summary>
        /// buffer
        /// </summary>
        public CombatBuffer Buffer { get; set; }

        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <returns></returns>
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

        public virtual string GetActionUid()
        {
            return Action.Uid;
        }
    }




}