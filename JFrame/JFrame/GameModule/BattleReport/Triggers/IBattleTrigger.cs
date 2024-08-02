using System;

namespace JFrame
{
    public enum BattleTriggerType
    {
        All,
        Normal,    
        AfterDead, //死亡后触发
    }
    public interface IBattleTrigger
    {
        BattleTriggerType TriggerType { get; }

        IBattleAction Owner { get; }

        void SetEnable(bool isOn);

        bool GetEnable();

        void Update(BattleFrame frame);

        void OnAttach(IBattleAction action);

        void Restart();

        bool IsOn();

        /// <summary>
        /// 获取CD
        /// </summary>
        /// <returns></returns>
        float[] GetArgs();

        /// <summary>
        /// 设置cd
        /// </summary>
        /// <param name="cd"></param>
        /// <exception cref="NotImplementedException"></exception>
        void SetArgs(float[] args);

        /// <summary>
        /// 设置无效
        /// </summary>
        void SetInValid();
    }
}