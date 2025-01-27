using System;

namespace JFrame
{
    /// <summary>
    /// 触发器
    /// </summary>
    public interface ICombatTrigger
    {
        /// <summary>
        /// 触发的消息
        /// 触发可传递的参数
        /// </summary>
        event Action<CombatExtraData> onTriggerOn;
    }


}