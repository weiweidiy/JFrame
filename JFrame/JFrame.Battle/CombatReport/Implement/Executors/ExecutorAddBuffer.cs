using System;

namespace JFrame
{
    /// <summary>
    ///  type=5 参数：0 执行周期 1: bufferId, 2: foldCount 3:duration
    /// </summary>
    public class ExecutorAddBuffer : ExecutorCombatNormal
    {
        public ExecutorAddBuffer(ICombatFinder combinFinder, ICombatFormula formula) : base(combinFinder, formula)
        {
        }

        public override int GetValidArgsCount()
        {
            return 4;
        }

        protected int GetBuffIdArg()
        {
            return (int)GetCurArg(1);
        }


        protected int GetBuffFoldArg()
        {
            return (int)GetCurArg(2);
        }

        protected int GetBuffDurationArg()
        {
            return (int)GetCurArg(3);
        }

        protected override double GetExecutorValue()
        {
            return GetBuffIdArg();
        }



        protected override void DoHit(CombatUnit target, CombatExtraData extraData)
        {
            var clone = extraData.Clone() as CombatExtraData;
            var buffer = context.CombatBufferFactory.CreateBuffer(GetBuffIdArg(), clone);
            buffer.SetCurFoldCount(GetBuffFoldArg());
            buffer.SetDuration(GetBuffDurationArg());
            buffer.OnAttach(target);

            target.AddBuffer(buffer);
        }


    }
}