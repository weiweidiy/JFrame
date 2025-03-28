using JFrame.Common;
using System.Collections.Generic;
using System.Reflection;

namespace JFrame
{
    /// <summary>
    /// type 4  参数0: 概率  参数1：目标（0：受击者  1：攻击者） 参数2：反击伤害触发（0=不触发 1=触发）
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
            return 3;
        }

        protected float GetRandomArg()
        {
            return GetCurArg(0);
        }

        protected int GetTargetType()
        {
            return (int)GetCurArg(1);
        }

        protected int GetTriggerType()
        {
            return (int)GetCurArg(2);
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

            foreach (var target in unitList)
            {
                target.onDamaged -= Target_onDamaging;
            }
        }

        private void Target_onDamaging(CombatExtraData data)
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
            {
                var targetType = GetTargetType();
                if (targetType == 0)
                    ExtraData.Targets = unitList;
                else
                {
                    ExtraData.Targets = new List<CombatUnit>() { data.Caster };
                    ExtraData.Target = ExtraData.Targets[0];
                    ExtraData.ExtraArg = data.Value; //受到的伤害
                }
            }

            if (GetTriggerType() == 0 && data.ValueType == CombatValueType.TurnBackDamage)
                return;

            SetOn(true);
        }


    }
}