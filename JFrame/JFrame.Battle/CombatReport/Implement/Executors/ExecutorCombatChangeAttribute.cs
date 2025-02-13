using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    ///  type = 4 参数：0 执行周期 1=attrId, 2=倍率
    /// </summary>
    public class ExecutorCombatChangeAttribute : ExecutorCombatNormal
    {
        string uid = "ExecutorCombatChangeAttribute";

        List<CombatUnit> targets = new List<CombatUnit>();

        public override int GetValidArgsCount()
        {
            return 3;
        }

        protected float GetAttrIdArg()
        {
            return (int)GetCurArg(1);
        }

        protected float GetRateArg()
        {
            return GetCurArg(2);
        }

        public ExecutorCombatChangeAttribute(ICombatFinder combinFinder, ICombatFormula formula) : base(combinFinder, formula)
        {
        }

        protected override double GetExecutorValue()
        {
            return GetRateArg();
        }

        protected override void DoHit(CombatUnit target, CombatExtraData data)
        {
            var itemAttr = target.GetAttribute((CombatAttribute)GetAttrIdArg());
            var finalValue = data.Value * itemAttr.CurValue;
            uid = data.Action.Uid;

            target.AddExtraValue((CombatAttribute)GetAttrIdArg(), uid, finalValue);
            targets.Add(target);
        }

        public override void OnStop()
        {
            base.OnStop();

            foreach(var target in targets)
            {
                target.RemoveExtraValue((CombatAttribute)GetAttrIdArg(), uid);
            }
        }
    }
}