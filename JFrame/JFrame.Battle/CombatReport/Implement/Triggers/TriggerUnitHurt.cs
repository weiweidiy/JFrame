using JFrame.Common;
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
        public TriggerUnitHurt(CombatBaseFinder finder) : base(finder)
        {
        }

        public override int GetValidArgsCount()
        {
            return 1;
        }

        public int GetTargetArg()
        {
            return (int)GetCurArg(0);
        }

        protected float GetRandomArg()
        {
            return GetCurArg(1);
        }


        public override void OnEnterState()
        {
            base.OnEnterState();
            unitList.Clear();

            if (finder != null)
            {
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

            ExtraData.Targets = unitList;
            SetOn(true);
        }


    }
}