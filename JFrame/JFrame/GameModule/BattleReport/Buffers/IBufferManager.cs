using System;

namespace JFrame
{
    public interface IBufferManager
    {
         event Action<IBuffer> onBufferUpdated;
         event Action<IBuffer> onBufferAdded;
         event Action<IBuffer> onBufferRemoved;
         event Action<IBuffer> onBufferCast;//buff触发效果了

        IBuffer AddBuffer(IBattleUnit target, int bufferId, int foldCout = 1);
        bool RemoveBuffer(string uid);

        void Update(BattleFrame frame);

        IBuffer[] GetBuffers();
    }
}