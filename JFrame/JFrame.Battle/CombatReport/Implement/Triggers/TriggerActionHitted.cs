using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// type 6 參數0：actionGroupId 参数1：sortid:  参数2: 概率  参数3：是否暴击（0全选， 1必须暴击）
    /// </summary>
    public class TriggerActionHitted : CombatBaseTrigger
    {
        List<CombatUnit> unitList = new List<CombatUnit>();

        public TriggerActionHitted(CombatBaseFinder finder) : base(finder)
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

        protected int GetRandomArg()
        {
            return (int)GetCurArg(2);
        }

        protected int GetCriArg()
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
            if (GetGroupIdArg() != 0 && extraData.Action.GroupId != GetGroupIdArg())
                return;

            if (GetSortIdArg() != 0 && extraData.Action.SortId != GetSortIdArg())
                return;

            if (GetCriArg() == 1 && !extraData.IsCri) //需要暴击，但是没有暴击
                return;

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
                target.onHittedTarget -= Target_onHittedTarget;
            }
        }
    }
}