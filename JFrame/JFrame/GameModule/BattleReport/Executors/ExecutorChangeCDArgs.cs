
using System.Collections.Generic;
using System.Diagnostics;

namespace JFrame
{

    /// <summary>
    /// type 13
    /// </summary>
    public class ExecutorChangeCDArgs : ExecutorNormal
    {
        public ExecutorChangeCDArgs(FormulaManager formulaManager, float[] args) : base(formulaManager, args)
        {
        }

        public override void Hit(IBattleUnit caster, IBattleAction action, List<IBattleUnit> target, object[] arg = null)
        {
            IBattleAction targetAction = arg[0] as IBattleAction;
            if (targetAction == null)
                throw new System.Exception("ExecutorChangeCDArgs 参数转换失败");

            targetAction.SetCdArgs(args);
        }


    }
}


