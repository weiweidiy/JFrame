namespace JFrame
{
    /// <summary>
    /// 眩晕 参数 1： 持续时间
    /// </summary>
    public class DeBufferStunning : DurationBuffer
    {
        public DeBufferStunning(IBattleUnit caster, string UID, int id, int foldCount, float[] args) : base(caster, UID, id, foldCount, args)
        {
        }

        public override void OnAttach(IBattleUnit unit)
        {
            base.OnAttach(unit);

            unit.OnStunning(ActionType.All, GetDuration());
        }

        public override void OnDettach()
        {
            base.OnDettach();

            target.OnResumeFromStunning(ActionType.All);
        }
    }
}




