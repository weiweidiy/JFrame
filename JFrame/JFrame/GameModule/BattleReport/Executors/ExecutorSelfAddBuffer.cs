﻿namespace JFrame
{
    /// <summary>
    /// 1：执行段数，2：延迟执行 3: 段数间隔 4：buffId 5:层数 type = 6
    /// </summary>
    public class ExecutorSelfAddBuffer : ExecutorTargetAddBuffer
    {
        public ExecutorSelfAddBuffer(float[] args) : base(args)
        {
        }

        /// <summary>
        /// 命中，开始添加buffer
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="action"></param>
        /// <param name="target"></param>
        public override void Hit(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            //添加buff
            caster.AddBuffer(bufferId, foldCount);
        }
    }
}