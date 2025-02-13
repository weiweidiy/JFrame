namespace JFrame
{
    /// <summary>
    ///   参数：0 执行周期 1: bufferId, 2: foldCount 3:duration
    /// </summary>
    public class ExecutorAddBuffer : ExecutorCombatNormal
    {
        public ExecutorAddBuffer(ICombatFinder combinFinder, ICombatFormula formula) : base(combinFinder, formula)
        {
        }

        public override int GetValidArgsCount()
        {
            return 3;
        }

        protected int GetBuffIdArg()
        {
            return (int)GetCurArg(1);
        }


        protected int GetBuffFold()
        {
            return (int)GetCurArg(2);
        }

        protected override double GetExecutorValue()
        {
            return GetBuffIdArg();
        }



        protected override void DoHit(CombatUnit target, CombatExtraData data)
        {
            target.AddBuffer()
        }


    }
}