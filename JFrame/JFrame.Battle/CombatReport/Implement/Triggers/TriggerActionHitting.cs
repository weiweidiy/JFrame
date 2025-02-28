using JFrame.Common;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// type 7 參數0：actionGroupId 参数1：sortid:  参数2: 概率  参数3：hp小于百分比  参数4：数值类型  参数5：倍率
    /// </summary>
    public class TriggerActionHitting : CombatBaseTrigger
    {
        List<CombatUnit> unitList = new List<CombatUnit>();

        Utility utility = new Utility();

        public TriggerActionHitting(CombatBaseFinder finder) : base(finder)
        {
        }

        public override int GetValidArgsCount()
        {
            return 6;
        }

        protected int GetGroupIdArg()
        {
            return (int)GetCurArg(0);
        }

        protected int GetSortIdArg()
        {
            return (int)GetCurArg(1);
        }

        protected int GetRandomArg()
        {
            return (int)GetCurArg(2);
        }

        protected float GetHpLessPercentArg()
        {
            return GetCurArg(3);
        }

        protected int GetValueTypeArg()
        {
            return (int)GetCurArg(4);
        }

        protected float GetValueRateArg()
        {
            return GetCurArg(5);
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
                        target.onHittingTarget += Target_onHittingTarget;
                        unitList.Add(target);
                    }
                }
            }
            else
            {
                ExtraData.Owner.onHittingTarget += Target_onHittingTarget;
                unitList.Add(ExtraData.Owner);
            }
        }

        private void Target_onHittingTarget(CombatExtraData extraData)
        {
            if (extraData.Action.Uid == ExtraData.Action.Uid)
                return;

            if (!utility.RandomHit(GetRandomArg() * 100))
                return;

            if (extraData.Target.GetHpPercent() > GetHpLessPercentArg())
                return;

            if (GetGroupIdArg() != 0 && extraData.Action.GroupId != GetGroupIdArg())
                return;

            if (GetSortIdArg() != 0 && extraData.Action.SortId != GetSortIdArg())
                return;

            if (extraData.ValueType != (CombatValueType)GetValueTypeArg())
                return;

            var lst = new List<CombatUnit>();
            if (extraData.Targets != null)
                lst.AddRange(extraData.Targets);

            ExtraData.Value = extraData.Value;
            ExtraData.Targets = lst;

            if (extraData.Target != null)
                ExtraData.Target = extraData.Target;

            extraData.Value *= GetValueRateArg();
            //SetOn(true);
        }



        public override void OnExitState()
        {
            base.OnExitState();

            foreach (var target in unitList)
            {
                target.onHittingTarget -= Target_onHittingTarget;
            }

            unitList.Clear();
        }

        public override void OnStop()
        {
            base.OnStop();

            foreach (var target in unitList)
            {
                target.onHittingTarget -= Target_onHittingTarget;
            }

            unitList.Clear();
        }
    }
}