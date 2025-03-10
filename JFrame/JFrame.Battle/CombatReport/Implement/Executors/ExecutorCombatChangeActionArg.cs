namespace JFrame
{
    /// <summary>
    /// type 9 更改action参数 参数0：执行周期， 参数1：组件类型（0:conditionFinder, 1:conditionTrigger，2：delayTrigger, 3:executorFinder , 4,formula, 5: executor, 6: cdTrigger）  参数2：组件索引    参数3： 参数索引   参数4： 正负加减值
    /// </summary>
    public class ExecutorCombatChangeActionArg : ExecutorCombatNormal
    {
        public ExecutorCombatChangeActionArg(CombatBaseFinder combinFinder, CombatBaseFormula formula) : base(combinFinder, formula)
        {
        }

        public override int GetValidArgsCount()
        {
            return 5;
        }

        protected int GetComponentType()
        {
            return (int)GetCurArg(1);
        }

        protected int GetComponentIndex()
        {
            return (int)GetCurArg(2);
        }

        protected int GetComponentArgIndex()
        {
            return (int)GetCurArg(3);
        }

        protected float GetComponentArgValue() //正负加减算法
        {
            return GetCurArg(4);
        }

        protected override void SetValueType(CombatExtraData data)
        {
            data.ValueType = CombatValueType.None;
        }

        //这个值
        protected override double GetExecutorValue()
        {
            return 0;
        }

        protected override void DoHit(CombatUnit target, CombatExtraData data)
        {
            var componentType = GetComponentType();
            var componentIndex = GetComponentIndex();
            var componentArgIndex = GetComponentArgIndex();
            var componentArgValue = GetComponentArgValue() * data.FoldCount;

            var actions = data.TargetActions; //收集到的所有技能
            foreach (var action in actions)
            {
                if(action.GetCurState() != nameof(ActionCdingState))
                    continue;

                //改变指定组件类型（暂时只有cdtrigger)
                if ((ActionComponentType)componentType == ActionComponentType.CdTrigger)
                {
                    var originValue = action.GetCdTriggerArg(componentIndex, componentArgIndex);
                    action.SetCdTriggerArg(componentIndex, componentArgIndex, componentArgValue + originValue);
                }
            }

            data.TargetActions.Clear();
        }

    }
}