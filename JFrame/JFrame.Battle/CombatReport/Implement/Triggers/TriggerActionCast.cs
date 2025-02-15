using JFrame.Common;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// type 5 目標釋放指定技能時觸發：  參數0：actionGroupId  参数1: 概率
    /// </summary>
    public class TriggerActionCast : CombatBaseTrigger
    {
        List<CombatUnit> unitList = new List<CombatUnit>();

        Utility utility = new Utility();

        public TriggerActionCast(CombatBaseFinder finder) : base(finder)
        {
        }

        public override int GetValidArgsCount()
        {
            return 2;
        }

        protected int GetGroupIdArg()
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
            if (!utility.RandomHit(GetRandomArg() * 100))
                return;

            if (extraData.Action.GroupId != GetGroupIdArg())
                return;

            ExtraData.Targets = new List<CombatUnit>() { extraData.Caseter };
            SetOn(true);
        }

        public override void OnExitState()
        {
            base.OnExitState();

            foreach (var target in unitList)
            {
                target.onActionCast -= Target_onActionCast;
            }
        }

    }
}