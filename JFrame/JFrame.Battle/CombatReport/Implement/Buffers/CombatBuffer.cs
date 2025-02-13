using System.Collections.Generic;

namespace JFrame
{
    public class CombatBuffer : BaseCombatBuffer
    {
        float delta;
        protected float duration;
        int curFoldCount;
        public override float GetDuration()
        {
            return duration;
        }

        public override void SetCurFoldCount(int foldCount) => curFoldCount = foldCount;
        public override int GetCurFoldCount() => curFoldCount;

        public override void Update(ComabtFrame frame)
        {
            delta += frame.DeltaTime;
            if (delta >= duration)
            {
                delta = 0f;
                Expired = true;
            }
        }

        public void Initialize(BufferInfo bufferInfo , List<CombatAction> actions)
        {
            FoldType = bufferInfo.foldType;
            Id = bufferInfo.id;
            duration = bufferInfo.duration;
            //curFoldCount = bufferInfo.foldCount;
            MaxFoldCount = bufferInfo.foldMaxCount;
        }
    }

}