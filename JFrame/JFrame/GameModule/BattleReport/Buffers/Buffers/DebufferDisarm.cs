﻿namespace JFrame
{
    /// <summary>
    /// 缴械 参数 1： 持续时间
    /// </summary>
    public class DebufferDisarm : DurationBuffer
    {
        public DebufferDisarm(IBattleUnit caster, string UID, int id, int foldCount, float[] args) : base(caster, UID, id, foldCount, args)
        {
        }

        public override void OnAttach(IBattleUnit unit)
        {
            base.OnAttach(unit);

            unit.OnStunning(ActionType.Normal, GetDuration());
        }

        public override void OnDettach()
        {
            base.OnDettach();

            target.OnResumeFromStunning(ActionType.Normal);
        }

    }
}



