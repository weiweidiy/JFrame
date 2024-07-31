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

        float GetCD();

        /// <summary>
        /// 设置无效
        /// </summary>
        void SetInValid();
    }
}