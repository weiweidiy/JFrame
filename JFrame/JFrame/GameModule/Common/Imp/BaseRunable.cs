using System;

namespace JFrame
{

    /// <summary>
    /// 抽象可运行对象
    /// </summary>
    public abstract class BaseRunable : IRunable
    {
        /// <summary>
        /// 完成通知
        /// </summary>
        public event Action<IRunable> onComplete;

        /// <summary>
        /// 通知完成
        /// </summary>
        /// <param name="runable"></param>
        protected void NotifyComplete(IRunable runable)=> onComplete?.Invoke(runable);

        /// <summary>
        /// 透传数据
        /// </summary>
        public RunableExtraData ExtraData { get; set; }

        /// <summary>
        /// 是否在运行
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="extraData"></param>
        /// <exception cref="Exception"></exception>
        public virtual void Run(RunableExtraData extraData)
        {
            if (IsRunning)
            {
                throw new Exception(this.GetType().ToString() + " is running , can't run again! ");
            }

            this.ExtraData = extraData;
            this.IsRunning = true;

            OnRun(extraData);
        }

        /// <summary>
        /// 子类实现生命周期 OnRun （相当于OnStart）
        /// </summary>
        /// <param name="extraData"></param>
        protected virtual void OnRun(RunableExtraData extraData) { }

        /// <summary>
        /// 停止运行
        /// </summary>
        public virtual void Stop()
        {
            NotifyComplete(this);
            IsRunning = false;
            OnStop();
        }

        /// <summary>
        /// 子类实现生命周期 (相当于OnDestroy)
        /// </summary>
        protected virtual void OnStop() { }

        /// <summary>
        /// 更新运行数据
        /// </summary>
        /// <param name="extraData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void Update(RunableExtraData extraData) { }
    }
}

