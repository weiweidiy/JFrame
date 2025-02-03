using System;

namespace JFrame
{
    /// <summary>
    /// 执行中状态
    /// </summary>
    public class ActionExecutingState : BaseActionState
    {
        public event Action onExecutingComplete;

    }
}
