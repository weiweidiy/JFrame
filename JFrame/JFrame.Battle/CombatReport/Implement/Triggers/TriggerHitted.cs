using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// type 6
    /// </summary>
    public class TriggerHitted : CombatBaseTrigger
    {
        List<CombatUnit> unitList = new List<CombatUnit>();

        public TriggerHitted(CombatBaseFinder finder) : base(finder)
        {
        }

        public override int GetValidArgsCount()
        {
            return 0;
        }

        //public override void OnEnterState()
        //{
        //    base.OnEnterState();
        //    unitList.Clear();
        //    if (finder != null)
        //    {
        //        var targets = finder.FindTargets(ExtraData); //获取目标
        //        if (targets != null && targets.Count > 0)
        //        {
        //            foreach (var target in targets)
        //            {
        //                target.on += Target_onActionCast;
        //                unitList.Add(target);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ExtraData.Owner.onActionCast += Target_onActionCast;
        //        unitList.Add(ExtraData.Owner);
        //    }
        //}

        //private void Target_onActionCast(CombatExtraData extraData)
        //{
        //    if (extraData.Action.GroupId == GetGroupIdArg())
        //    {
        //        ExtraData.Targets = new List<CombatUnit>() { extraData.Caseter };
        //        SetOn(true);
        //    }
        //}

        //public override void OnExitState()
        //{
        //    base.OnExitState();

        //    foreach (var target in unitList)
        //    {
        //        target.onActionCast -= Target_onActionCast;
        //    }
        //}
    }
}