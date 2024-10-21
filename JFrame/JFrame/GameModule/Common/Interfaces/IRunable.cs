using System;

namespace JFrame
{
    /// <summary>
    /// 透传参数数据
    /// </summary>
    public class RunableExtraData
    {
        public object Data { get; set; }
    }

    /// <summary>
    /// 可运行接口
    /// </summary>
    public interface IRunable
    {
        event Action<IRunable> onComplete;

        RunableExtraData ExtraData { get; set; }

        bool IsRunning { get; set; }

        void Run(RunableExtraData extraData);

        void Update(RunableExtraData extraData);

        void Stop();


    }

    
}

