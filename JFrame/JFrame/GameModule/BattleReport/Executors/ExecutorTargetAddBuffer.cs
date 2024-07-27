
using System;
using System.Collections.Generic;

namespace JFrame
{
    /// <summary>
    /// 1：执行段数，2：延迟执行 3: 段数间隔 4：buffId 5:层数 6:概率0-1 type = 2
    /// </summary>
    public class ExecutorTargetAddBuffer : BaseExecutor
    {
        protected int bufferId;
        protected int foldCount;
        protected float rate;//添加概率

        /// <summary>
        /// 第四个参数是bufferID, 第5个参数是buffer值
        /// </summary>
        /// <param name="args"></param>
        public ExecutorTargetAddBuffer(float[] args) : base(args)
        {
            if (args != null && args.Length >= 6)
            {
                bufferId = (int)args[3];
                foldCount = (int)args[4];
                rate = (float)args[5];
            }
            else
            {
                throw new System.Exception("添加buffer executor 参数数量不对 ");
            }
        }

        /// <summary>
        /// 命中，开始添加buffer
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="target"></param>
        public override void Hit(IBattleUnit caster, IBattleAction action, List<IBattleUnit> targets)
        {
            foreach(IBattleUnit target in targets)
            {
                var r = new Random().NextDouble();
                if (r >= rate)
                    return;

                //添加buff
                target.AddBuffer(bufferId, foldCount);
            }
   
        }
    }
}