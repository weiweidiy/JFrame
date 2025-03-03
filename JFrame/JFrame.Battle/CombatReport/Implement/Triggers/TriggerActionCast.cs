using JFrame.Common;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// type 5 目標釋放指定技能時觸發：  參數0：actionGroupId 参数1：sortID  参数2: 概率  参数3：触发所需次数
    /// </summary>
    public class TriggerActionCast : CombatBaseTrigger
    {
        List<CombatUnit> unitList = new List<CombatUnit>();

        Utility utility = new Utility();

        int countCondition = 1;

        public TriggerActionCast(CombatBaseFinder finder) : base(finder)
        {
        }

        public override int GetValidArgsCount()
        {
            return 4;
        }

        protected int GetGroupIdArg()
        {
            return (int)GetCurArg(0);
        }

        protected int GetSortIdArg()
        {
            return (int)GetCurArg(1);
        }

        protected float GetRandomArg()
        {
            return GetCurArg(2);
        }

        protected int GetCountCondition()
        {
            return (int)GetCurArg(3);
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
                        target.onActionCast += Target_onActionCast;
                        unitList.Add(target);
                    }
                }
            }
            else
            {
                ExtraData.Owner.onActionCast += Target_onActionCast;
                unitList.Add(ExtraData.Owner);
            }
        }

        private void Target_onActionCast(CombatExtraData extraData)
        {
            if (extraData.Action.Uid == ExtraData.Action.Uid)
                return;

            if (!utility.RandomHit(GetRandomArg() * 100))
                return;

            if (GetGroupIdArg() != 0 && extraData.Action.GroupId != GetGroupIdArg())
                return;

            if (GetSortIdArg() != 0 && extraData.Action.SortId != GetSortIdArg())
                return;

            if(countCondition < GetCountCondition())
            {
                countCondition++;
                return;
            }

            var lst = new List<CombatUnit>();
            if (extraData.Targets != null)
                lst.AddRange(extraData.Targets);

            ExtraData.Targets = lst;

            if (extraData.Target != null)
                ExtraData.Target = extraData.Target;

            SetOn(true);
        }

        public override void OnExitState()
        {
            base.OnExitState();

            foreach (var target in unitList)
            {
                target.onActionCast -= Target_onActionCast;
            }

            countCondition = 1;
        }

    }
}