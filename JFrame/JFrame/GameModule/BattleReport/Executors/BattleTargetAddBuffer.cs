
namespace JFrame
{
    /// <summary>
    /// 1：执行段数，2：延迟执行 3: 段数间隔 4：buffId 5:层数
    /// </summary>
    public class BattleTargetAddBuffer : BaseBattleExecutor
    {
        int bufferId;
        int foldCount;
        /// <summary>
        /// 第四个参数是bufferID, 第5个参数是buffer值
        /// </summary>
        /// <param name="args"></param>
        public BattleTargetAddBuffer(float[] args) : base(args)
        {
            if (args != null && args.Length >= 5)
            {
                bufferId = (int)args[3];
                foldCount = (int)args[4];
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
        public override void Hit(IBattleUnit caster, IBattleAction action, IBattleUnit target)
        {
            //添加buff
            target.AddBuffer(bufferId, foldCount);
        }
    }
}