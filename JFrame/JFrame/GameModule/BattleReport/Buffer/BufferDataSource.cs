using System;

namespace JFrame
{
    public class BufferDataSource
    {
        public virtual bool IsBuff(int buffId)
        {
            return true;
        }

        public virtual BufferFoldType GetFoldType(int buffId)
        {
            return BufferFoldType.Fold;
        }

        public virtual float GetArg(int buffId)
        {
            return 10;
        }

        public virtual float GetDuration(int buffId)
        {
            return 10;
        }

        public virtual int GetMaxValue(int buffId)
        {
            return 10;
        }

        public BufferTriggerType GetTriigerType(int bufferId)
        {
            return BufferTriggerType.OnDamage;
        }
    }
}