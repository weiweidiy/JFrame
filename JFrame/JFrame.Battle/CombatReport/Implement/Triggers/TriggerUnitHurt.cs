﻿using JFrame.Common;
using System.Collections.Generic;
using System.Reflection;

namespace JFrame
{
    /// <summary>
    /// type 4  参数0: 概率
    /// </summary>
    public class TriggerUnitHurt : CombatBaseTrigger
    {
        List<CombatUnit> unitList = new List<CombatUnit>();

        Utility utility = new Utility();
        public TriggerUnitHurt(List<CombatBaseFinder> finders) : base(finders)
        {
        }

        public override int GetValidArgsCount()
        {
            return 1;
        }

        protected float GetRandomArg()
        {
            return GetCurArg(0);
        }


        public override void OnEnterState()
        {
            base.OnEnterState();
            unitList.Clear();

            if (finders != null && finders.Count > 0)
            {
                var finder = finders[0];

                var targets = finder.FindTargets(ExtraData); //获取目标
                if (targets != null && targets.Count > 0)
                {
                    foreach (var target in targets)
                    {
                        target.onDamaged += Target_onDamaging;
                        unitList.Add(target);
                    }
                }
            }
            else
            {
                ExtraData.Owner.onDamaged += Target_onDamaging;
                unitList.Add(ExtraData.Owner);
               
            }
            
        }


        public override void OnExitState()
        {
            base.OnExitState();

            foreach(var target in unitList)
            {
                target.onDamaged -= Target_onDamaging;
            }
        }

        private void Target_onDamaging(CombatExtraData obj)
        {
            if (!utility.RandomHit(GetRandomArg() * 100))
                return;

            if (finders != null && finders.Count > 1)
            {
                var finder = finders[1];
                var targets = finder.FindTargets(ExtraData);
                ExtraData.Targets = targets;
            }
            else
                ExtraData.Targets = unitList;
            SetOn(true);
        }


    }
}