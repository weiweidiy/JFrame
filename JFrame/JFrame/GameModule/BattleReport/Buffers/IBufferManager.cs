using System;

namespace JFrame
{
    public interface IBufferManager
    {
         event Action<IBuffer> onBufferUpdated;
         event Action<IBuffer> onBufferAdded;
         event Action<IBuffer> onBufferRemoved;
         event Action<IBuffer> onBufferCast;//buff触发效果了

        /// <summary>
        /// 添加一个指定buffer到指定单位上
        /// </summary>
        /// <param name="target"></param>
        /// <param name="bufferId"></param>
        /// <param name="foldCout"></param>
        /// <returns></returns>
        IBuffer AddBuffer(IBattleUnit target, int bufferId, int foldCout = 1);

        /// <summary>
        /// 移除一个指定buffer
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        bool RemoveBuffer(string uid);

        void Update(BattleFrame frame);

        /// <summary>
        /// 获取所有buffers
        /// </summary>
        /// <returns></returns>
        IBuffer[] GetBuffers();

        /// <summary>
        /// 清理所有buffers
        /// </summary>
        void Clear();
    }
}