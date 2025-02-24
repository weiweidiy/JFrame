using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// type 6 參數0：actionGroupId  参数1: 概率
    /// </summary>
    public class TriggerActionHitted : CombatBaseTrigger
    {
        List<CombatUnit> unitList = new List<CombatUnit>();

        public TriggerActionHitted(CombatBaseFinder finder) : base(finder)
        {
        }

        public override int GetValidArgsCount()
        {
            return 1;
        }

        protected int GetGroupIdArg()
        {
            return (int)GetCurArg(0);
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
                        target.onHittedTarget += Target_onHittedTarget;
                        unitList.Add(target);
                    }
                }
            }
            else
            {
                ExtraData.Owner.onHittedTarget += Target_onHittedTarget;
                unitList.Add(ExtraData.Owner);
            }
        }

        private void Target_onHittedTarget(CombatExtraData extraData)
        {
            if (extraData.Action.GroupId == GetGroupIdArg() && GetGroupIdArg() != 0)
            {
                //ExtraData.Targets = new List<CombatUnit>() { extraData.Caseter };
                SetOn(true);
            }
        }

        //private void Target_onActionCast(CombatExtraData extraData)
        //{
        //    if (extraData.Action.GroupId == GetGroupIdArg())
        //    {
        //        ExtraData.Targets = new List<CombatUnit>() { extraData.Caseter };
        //        SetOn(true);
        //    }
        //}

        public override void OnExitState()
        {
            base.OnExitState();

            foreach (var target in unitList)
            {
                target.onHittedTarget -= Target_onHittedTarget;
            }
        }
    }
}